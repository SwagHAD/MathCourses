namespace Domain.Interfaces
{
    public interface ISelectionService<TEntity>
    {
        Task GetQaeryResultAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query,
            CancellationToken ct = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
