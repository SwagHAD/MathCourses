using Application.DTO.Base;
using Domain.Entities.Base;
using Domain.Interfaces.Data;

namespace Application.Services.UnitOfWork
{
    public class UnitOfWork<TEntity, TDto>(IMathDbContext dbContext) : IUnitOfWork<TEntity, TDto> where TEntity : BaseEntity
        where TDto : IDataTransferObjectBase<TEntity>
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
