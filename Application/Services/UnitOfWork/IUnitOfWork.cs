using Application.DTO.Base;
using Domain.Entities.Base;

namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork<TEntity, TDto> where TEntity : BaseEntity
        where TDto : IDataTransferObjectBase<TEntity>
    {
        ValueTask<TEntity> ExecuteAsync(Func<TDto, Task<TEntity>> Action, bool IsAtomicOperation = true);
    }
}
