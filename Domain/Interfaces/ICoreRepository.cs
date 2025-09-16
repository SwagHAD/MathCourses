using Domain.Entities.Base;

namespace Domain.Interfaces
{
    public interface ICoreRepository<TEntity> : ISelectionService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateItemAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> CreateItemNoTransactionAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> UpdateItemAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> UpdateItemNoTransactionAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteItemAsync(int id, CancellationToken ct = default);
        Task DeleteItemNoTransactionAsync(int id, CancellationToken ct = default);
        Task<TEntity[]> CreateItemsAsync(TEntity[] entities, CancellationToken ct = default);
        Task<TEntity[]> CreateItemsNoTransactionAsync(TEntity[] entities, CancellationToken ct = default);
        Task<TEntity[]> UpdateItemsAsync(TEntity[] entities, CancellationToken ct = default);
        Task<TEntity[]> UpdateItemsNoTransactionAsync(TEntity[] entities, CancellationToken ct = default);
        Task DeleteItemsAsync(int[] entities, CancellationToken ct = default);
        Task DeleteItemsNoTransactionAsync(int[] entities, CancellationToken ct = default);
    }
}
