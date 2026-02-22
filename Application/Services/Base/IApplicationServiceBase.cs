using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TEntity>> CreateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : IDtoBaseCreate<TEntity>;
        Task<Response<bool>> DeleteItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : IDtoBaseDelete<TEntity>;
        Task<Response<TEntity>> UpdateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : IDtoBaseUpdate<TEntity>;
    }
}
