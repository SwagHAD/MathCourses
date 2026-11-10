using Application.Access;
using Domain.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Access
{
    /// <summary>
    /// Держит access_tuples в согласии с доменными таблицами: перед каждым SaveChanges
    /// считает по изменённым сущностям разницу кортежей «было/стало» и добавляет её в тот же SaveChanges.
    /// Итог — кортежи и домен меняются одной транзакцией, отдельной синхронизации не нужно.
    /// Не видит ExecuteUpdate/ExecuteDelete и каскады на стороне БД: после таких операций — RebuildAccessTuples.
    /// </summary>
    internal sealed class AccessTupleInterceptor : SaveChangesInterceptor
    {
        private readonly IReadOnlyDictionary<Type, IAccessTupleProjection> _projections;

        public AccessTupleInterceptor(IEnumerable<IAccessTupleProjection> projections)
        {
            _projections = projections.ToDictionary(p => p.EntityType);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is { } context)
                ApplyAsync(context, Collect(context), async: false, CancellationToken.None).GetAwaiter().GetResult();
            return result;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is { } context)
                await ApplyAsync(context, Collect(context), async: true, cancellationToken);
            return result;
        }

        private TupleChanges Collect(DbContext context)
        {
            var changes = new TupleChanges();
            if (_projections.Count == 0)
                return changes;

            // Перехватчик вызывается до автоматического DetectChanges — без него не увидим правки свойств
            context.ChangeTracker.DetectChanges();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                    continue;
                if (!_projections.TryGetValue(entry.Metadata.ClrType, out var projection))
                    continue;

                var before = entry.State == EntityState.Added ? [] : Project(projection, entry.OriginalValues);
                var after = entry.State == EntityState.Deleted ? [] : Project(projection, entry.CurrentValues);

                changes.Remove.UnionWith(before.Except(after));
                changes.Add.UnionWith(after.Except(before));
            }

            // Одна сущность убрала кортеж, другая в том же сохранении его вернула — кортеж просто остаётся
            var both = changes.Add.Intersect(changes.Remove).ToList();
            changes.Add.ExceptWith(both);
            changes.Remove.ExceptWith(both);
            return changes;
        }

        private static HashSet<TupleKey> Project(IAccessTupleProjection projection, PropertyValues values) =>
            projection.Project(values.ToObject()).ToHashSet();

        private static async Task ApplyAsync(DbContext context, TupleChanges changes, bool async, CancellationToken ct)
        {
            var tuples = context.Set<AccessTuple>();

            foreach (var key in changes.Remove)
            {
                var query = Match(tuples, key);
                var existing = async ? await query.ToListAsync(ct) : query.ToList();
                tuples.RemoveRange(existing);
            }

            foreach (var key in changes.Add)
            {
                // Идемпотентно: кортеж мог появиться раньше — перестройкой или параллельной правкой
                var query = Match(tuples, key);
                var exists = async ? await query.AnyAsync(ct) : query.Any();
                if (!exists)
                    tuples.Add(AccessTuple.Create(key));
            }
        }

        private static IQueryable<AccessTuple> Match(IQueryable<AccessTuple> tuples, TupleKey key) =>
            tuples.Where(t => !t.IsReplica
                && t.ObjectType == key.ObjectType && t.ObjectId == key.ObjectId && t.Relation == key.Relation
                && t.SubjectType == key.SubjectType && t.SubjectId == key.SubjectId
                && t.SubjectRelation == key.SubjectRelation);

        private sealed class TupleChanges
        {
            public HashSet<TupleKey> Add { get; } = [];
            public HashSet<TupleKey> Remove { get; } = [];
        }
    }
}
