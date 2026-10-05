using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel.Application.Interfaces;
using SharedKernel.Persistence.Extensions;

namespace SharedKernel.Persistence
{
    /// <summary>
    /// Базовая реализация <see cref="ISwagDbContext"/>: транзакции, миграции и общая настройка модели.
    /// Конфигурации сущностей подхватываются из сборки, где объявлен конкретный контекст.
    /// </summary>
    public abstract class BaseDbContext(DbContextOptions options) : DbContext(options), ISwagDbContext
    {
        private IDbContextTransaction? _currentTransaction;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null || Database.CurrentTransaction == null)
            {
                _currentTransaction = Database.CurrentTransaction ?? await Database.BeginTransactionAsync(cancellationToken);
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellationToken);
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
                else
                {
                    throw new InvalidOperationException("Transaction was not started");
                }
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            // Без исключения, если транзакции нет: откат вызывается из catch и не должен скрывать исходную ошибку
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            await Database.MigrateAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ConfigureBaseEntities();
        }
    }
}
