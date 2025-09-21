using Domain.Entities.Base;
using Domain.Entities.Pagination;

namespace Domain.Interfaces
{
    public interface ISelectionService<TEntity> where TEntity : BaseEntity
    {
        #region Select
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken ct = default);
        #endregion
    }
}
