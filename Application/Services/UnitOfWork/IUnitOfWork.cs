using Application.DTO.Base;
using Domain.Entities.Base;

namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork<TEntity, TDto> where TEntity : BaseEntity
        where TDto : IDataTransferObjectBase<TEntity>
    {
        Task<TEntity> ExecuteAsync(Func<Task<TEntity>> Action, bool IsAtomicOperation = true);
        Task ExecuteAsync(Func<Task> Action, bool IsAtomicOperation = true);
    }
}
