namespace Application.Handlers.Base
{
    public interface IHandlerFactory<TEntity>
    {
        IHandler<TEntity, TDto> GetHandle<TDto>();
    }
}
