using Application.Base;
using Application.Command.Base;
using Application.Responses.Base;
using Domain.Entities.Base;

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
        public async Task<Response<TResponse>> CreateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : IBaseRequestCreate<TEntity> 
            where TResponse : IBaseResponse<TEntity>
        {
            return await Handle<TRequest, TResponse>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send(dto, cancellationToken);
                }, isAtomicOperation);
            });
        }

        public async Task<Response<TResponse>> DeleteItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default) 
            where TRequest : IBaseRequestDelete<TEntity> 
            where TResponse: IBaseResponse<TEntity>
        {
            return await Handle<TRequest, TResponse>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send(dto, cancellationToken);
                }, isAtomicOperation);
            });
        }
        public async Task<Response<TResponse>> UpdateItemAsync<TRequest, TResponse>(TRequest dto, bool isAtomicOperation = true, CancellationToken cancellationToken = default)
            where TRequest : IBaseRequestUpdate<TEntity> 
            where TResponse : IBaseResponse<TEntity>
        {
            return await Handle<TRequest, TResponse>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send(dto, cancellationToken);
                }, isAtomicOperation);
            });
        }
    }
}
