using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOutboxContext : IAsyncDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task AddMessageAsync(OutboxMessage message);
        Task<OutboxMessage[]> GetPendingMessagesAsync(int batchSize, int maxRetryCount, CancellationToken ct = default);
    }
}
