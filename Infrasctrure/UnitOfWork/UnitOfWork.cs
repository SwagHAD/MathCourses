using Domain.Entities.Base;
using Domain.Interfaces.Data;
using Domain.Interfaces.UnitOfWork;

namespace Infrastructure.UnitOfWork
{
    internal class UnitOfWork<TEntity>(IMathDbContext dbContext) : IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        public async Task<TEntity> ExecuteAsync(Func<Task<TEntity>> Action, bool IsAtomicOperation = true)
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
