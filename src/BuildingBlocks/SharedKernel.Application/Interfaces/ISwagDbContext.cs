using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SharedKernel.Application.Interfaces
{
    /// <summary>
    /// Общий контракт контекста БД для всех микросервисов.
    /// Таблицы берутся через <see cref="Set{TEntity}"/>, базовая реализация — BaseDbContext в SharedKernel.Persistence.
    /// </summary>
    public interface ISwagDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task MigrateAsync(CancellationToken cancellationToken = default);
    }
}
