using Application.Base;
using Application.Command.Base;
using Application.Responses.Base;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface ICrudServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TResponse>> CreateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : IBaseRequestCreate<TEntity> where TResponse : IBaseResponse<TEntity>;
        Task<Response<TResponse>> DeleteItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : IBaseRequestDelete<TEntity> where TResponse : IBaseResponse<TEntity>;
        Task<Response<TResponse>> UpdateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : IBaseRequestUpdate<TEntity> where TResponse : IBaseResponse<TEntity>;
    }
}
