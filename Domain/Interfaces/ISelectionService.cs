namespace Domain.Interfaces
{
    public interface ISelectionService<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
