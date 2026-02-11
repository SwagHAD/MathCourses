namespace Application.Handlers.Base
{
    public interface IHandlerFactory<TEntity>
    {
        IHandler<TEntity> GetHandle();
    }
}
