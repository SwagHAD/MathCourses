using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface ICrudServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TResponse>> CreateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : ICommandBaseCreate<TEntity> where TResponse : ICommandBase<TEntity>;
        Task<Response<TResponse>> DeleteItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : ICommandBaseDelete<TEntity> where TResponse : ICommandBase<TEntity>;
        Task<Response<TResponse>> UpdateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : ICommandBaseUpdate<TEntity> where TResponse : ICommandBase<TEntity>;
    }
}
