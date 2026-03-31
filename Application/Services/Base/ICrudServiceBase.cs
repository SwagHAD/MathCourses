using Application.Base;
using Application.Responses.Base;
using Domain.Entities.Base;
using MediatR;

namespace Application.Services.Base
{
    public interface ICrudServiceBase<TEntity> where TEntity : BaseEntity
    {
        public Task<Response<TResponse>> ExecuteAsync<TRequest, TResponse>(TRequest dto, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse> where TResponse : IResponse<TEntity>;
        public Task<Response<TResponse>> ExecuteNoTransactionAsync<TRequest, TResponse>(TRequest dto, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse> where TResponse : IResponse<TEntity>;
    }
}
