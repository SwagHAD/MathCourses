namespace Application.Handlers.Base
{
    public interface IHandler<TEntity, TDto>
    {
        Task<TEntity> Handle(TDto dto);
    }
}
