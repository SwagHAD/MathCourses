using Domain.Entities.Base;

namespace Domain.Interfaces
{
    public interface ICoreRepository<TEntity> : ISelectionService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
