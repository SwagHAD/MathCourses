using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TEntity>> CreateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseCreate<TEntity>;
        Task<Response<bool>> DeleteItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseDelete<TEntity>;
        Task<Response<TEntity>> UpdateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseUpdate<TEntity>;
    }
}
