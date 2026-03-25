using Domain.Interfaces.Data;
using Domain.Interfaces.UnitOfWork;

namespace Infrastructure
{
    internal class UnitOfWork(ISwagDbContext dbContext) : IUnitOfWork
    {
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> Action, bool IsAtomicOperation = true)
        {
            if (!IsAtomicOperation)
              return await Action();
            await dbContext.BeginTransactionAsync();
            try
            {
                var entity = await Action();
                await dbContext.CommitTransactionAsync();
                return entity;
            }
            catch
            {
                await dbContext.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> Action, bool IsAtomicOperation = true)
        {
            if (!IsAtomicOperation)
            {
                await Action();
                return;
            }
            await dbContext.BeginTransactionAsync();
            try
            {
                await Action();
                await dbContext.CommitTransactionAsync();
            }
            catch
            {
                await dbContext.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
