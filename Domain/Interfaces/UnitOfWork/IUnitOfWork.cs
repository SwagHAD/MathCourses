namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> Action, bool IsAtomicOperation = true);
        Task ExecuteAsync(Func<Task> Action, bool IsAtomicOperation = true);
    }
}
