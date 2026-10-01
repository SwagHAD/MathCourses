using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StreamingService.Domain.Entities;

namespace StreamingService.Application.Common.Interfaces
{
    public interface IStreamDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<StreamSession> StreamSessions { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken cancellation = default);
        Task CommitTransactionAsync(CancellationToken cancellation = default);
        Task RollbackTransactionAsync(CancellationToken cancellation = default);
        Task MigrateAsync(CancellationToken cancellationToken = default);
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
    }
}
