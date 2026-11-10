using Domain.Access;
using SharedKernel.Application.Interfaces;

namespace Application.Access
{
    /// <summary>Пара «тип объекта + отношение», которую проекция считает своей.</summary>
    public readonly record struct OwnedRelation(AccessObjectType ObjectType, string Relation);

    /// <summary>
    /// Правило вывода кортежей из доменной сущности. Используется двумя способами:
    /// перехватчиком SaveChanges (разница «было/стало» по одной записи) и полной перестройкой.
    /// </summary>
    public interface IAccessTupleProjection
    {
        Type EntityType { get; }

        /// <summary>
        /// Отношения, которые целиком принадлежат проекции: при перестройке они удаляются и создаются заново.
        /// Явные выдачи с такими же отношениями держать нельзя — их сотрёт перестройка.
        /// </summary>
        IReadOnlyCollection<OwnedRelation> OwnedRelations { get; }

        /// <summary>Кортежи одного состояния сущности. Пусто — сущность в граф не попадает.</summary>
        IEnumerable<TupleKey> Project(object entity);

        /// <summary>Кортежи по всем сущностям таблицы — для перестройки.</summary>
        Task<IReadOnlyList<TupleKey>> ProjectAllAsync(ISwagDbContext dbContext, CancellationToken cancellationToken);
    }
}
