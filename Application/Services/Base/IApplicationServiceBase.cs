using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TEntity>> CreateItemAsync(IDtoBaseCreate<TEntity> dto, bool IsAtomicOperation = true);
        Task<Response<bool>> DeleteItemAsync(IDtoBaseDelete<TEntity> dto, bool IsAtomicOperation = true);
        Task<Response<TEntity>> UpdateItemAsync(IDtoBaseUpdate<TEntity> dto, bool IsAtomicOperation = true);
    }
}
