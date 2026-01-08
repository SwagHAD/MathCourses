using Domain.Entities.Base;

namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork<TEntity>
    {
        Task<TEntity> ExecuteAsync(Func<Task<TEntity>> Action, bool IsAtomicOperation = true);
        Task ExecuteAsync(Func<Task> Action, bool IsAtomicOperation = true);
    }
}
