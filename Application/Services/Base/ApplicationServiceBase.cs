using Application.Base;
using Application.Responses.Base;
using Domain.Entities.Base;
using MediatR;

namespace Application.Services.Base
{
    /// <summary>
    /// Базовый класс по создание бизнес кейсов
    /// Классы наследники помечать как sealed
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы</typeparam>
    /// <param name="services">Базовый сервис</param>
    public partial class CrudServiceBase<TEntity>(IServiceProvider services) : ICrudServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        public async Task<Response<TResponse>> ExecuteAsync<TRequest, TResponse>(TRequest dto, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse<TEntity>
        {
            return await Handle<TRequest, TResponse>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send<TResponse>(dto, cancellationToken);
                }, true);
            });
        }

        public async Task<Response<TResponse>> ExecuteNoTransactionAsync<TRequest, TResponse>(TRequest dto, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse<TEntity>
        {
            return await Handle<TRequest, TResponse>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send<TResponse>(dto, cancellationToken);
                }, false);
            });
        }
    }
}
