using Application.DTO.Base;
using Domain.Entities.Base;

namespace Application.Handlers.Base
{
    public interface ICreateHandler<TEntity, TDto> : IHandler<TEntity, TDto> 
        where TEntity : BaseEntity where TDto : IDtoBaseCreate<TEntity>
    {
    }
}
