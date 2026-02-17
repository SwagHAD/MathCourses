using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<int>> CreateItemAsync(IDtoBaseCreate<TEntity> dto, bool IsAtomicOperation = true);
        Task<Response<bool>> DeleteItemAsync(IDtoBaseDelete<TEntity> dto, bool IsAtomicOperation = true);
        Task<Response<int>> UpdateItemAsync(IDtoBaseUpdate<TEntity> dto, bool IsAtomicOperation = true);
    }
}
