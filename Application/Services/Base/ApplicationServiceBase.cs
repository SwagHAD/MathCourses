using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    /// <summary>
    /// Базовый класс по создание бизнес кейсов
    /// Классы наследники помечать как sealed
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы</typeparam>
    /// <param name="services">Базовый сервис</param>
    public partial class ApplicationServiceBase<TEntity>(IServiceProvider services) : IApplicationServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        public async Task<Response<TEntity>> CreateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseCreate<TEntity>
        {
            return await Handle(async() =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send<TEntity>(dto);
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<bool>> DeleteItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseDelete<TEntity>
        {
            return await Handle(async () =>
            {
                await UnitOfWork.ExecuteAsync(async () =>
                {
                    await Mediator.Send(dto);
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<TEntity>> UpdateItemAsync<TCommand>(TCommand dto, bool IsAtomicOperation = true) where TCommand : ICommandBaseUpdate<TEntity>
        {
            return await Handle(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send<TEntity>(dto);
                }, IsAtomicOperation);
            }, dto);
        }
    }
}
