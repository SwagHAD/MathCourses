using Domain.Entities.Base;

namespace Domain.Interfaces
{
    public interface ICoreRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> DeleteAsync(int id, CancellationToken ct = default);
    }
}
