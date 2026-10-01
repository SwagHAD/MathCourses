using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StreamingService.Application.Common.Interfaces;
using StreamingService.Domain.Entities;

namespace StreamingService.Infrastructure.Postgres
{
    internal sealed class StreamDbContext : DbContext, IStreamDbContext
    {
        private IDbContextTransaction? _currentTransaction;
        public DbSet<StreamSession> StreamSessions { get; set; }
        public StreamDbContext(DbContextOptions<StreamDbContext> options) : base(options) { }
        public async Task BeginTransactionAsync(CancellationToken cancellation = default)
        {
            if (_currentTransaction == null || Database.CurrentTransaction == null)
                _currentTransaction = Database.CurrentTransaction 
                    ?? await Database.BeginTransactionAsync(cancellation);
        }
        public async Task CommitTransactionAsync(CancellationToken cancellation = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellation);
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
                else
                {
                    throw new Exception("Transaction was not started");
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellation = default)
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellation);
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
            else
            {
                throw new Exception("Transaction was not started");
            }
        }
        public async Task MigrateAsync(CancellationToken cancellationToken = default) 
            => await Database.MigrateAsync(cancellationToken);
    }
}
