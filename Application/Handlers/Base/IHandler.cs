namespace Application.Handlers.Base
{
    public interface IHandler<TEntity>
    {
        Task<TEntity> Handle<TDto>(TDto dto);
    }
}
