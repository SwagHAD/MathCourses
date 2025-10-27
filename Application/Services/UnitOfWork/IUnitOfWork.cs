namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit(CancellationToken ct);
        Task RollBack(CancellationToken ct);
        Task BeginTransaction(CancellationToken ct);
    }
}
