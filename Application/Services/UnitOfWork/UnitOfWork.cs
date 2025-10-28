using Application.DTO.Base;
using Domain.Entities.Base;
using Domain.Interfaces.Data;

namespace Application.Services.UnitOfWork
{
    public class UnitOfWork<TEntity, TDto>(IMathDbContext dbContext) : IUnitOfWork<TEntity, TDto> where TEntity : BaseEntity
        where TDto : IDataTransferObjectBase<TEntity>
    {
        public async ValueTask<TEntity> ExecuteAsync(Func<TDto, Task<TEntity>> Action, bool IsAtomicOperation = true)
        {
            if (Action is null)
                throw new ArgumentException("Операция не определена");
            try
            {
                if (IsAtomicOperation)
                    await dbContext.BeginTransactionAsync();
                var entity = await Action();
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
