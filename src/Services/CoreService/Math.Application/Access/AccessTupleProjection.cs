using Domain.Access;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Access
{
    /// <summary>Типизированная основа проекции: наследнику остаётся описать Project и, при желании, Source.</summary>
    public abstract class AccessTupleProjection<TEntity> : IAccessTupleProjection where TEntity : class
    {
        public Type EntityType => typeof(TEntity);

        public abstract IReadOnlyCollection<OwnedRelation> OwnedRelations { get; }

        public IEnumerable<TupleKey> Project(object entity) => Project((TEntity)entity);

        protected abstract IEnumerable<TupleKey> Project(TEntity entity);

        /// <summary>Отсев в SQL для перестройки, чтобы не тянуть строки, которые всё равно дадут пусто.</summary>
        protected virtual IQueryable<TEntity> Source(IQueryable<TEntity> query) => query;

        public async Task<IReadOnlyList<TupleKey>> ProjectAllAsync(ISwagDbContext dbContext, CancellationToken cancellationToken)
        {
            // Список, а не поток: перестройка пишет в тот же контекст, а Npgsql не держит
            // открытый reader и insert на одном соединении одновременно
            var entities = await Source(dbContext.Set<TEntity>().AsNoTracking()).ToListAsync(cancellationToken);
            return entities.SelectMany(Project).ToList();
        }
    }
}
