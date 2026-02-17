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
    /// <typeparam name="TDtoBase">Базовая ДТО</typeparam>
    /// <param name="services">Базовый сервис</param>
    public partial class ApplicationServiceBase<TEntity>(IServiceProvider services) : IApplicationServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        public async Task<Response<TEntity>> CreateItemAsync(IDtoBaseCreate<TEntity> dto, bool IsAtomicOperation = true)
        {
            return await Handle(async() =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await Mediator.Send<TEntity>(dto);
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<bool>> DeleteItemAsync(IDtoBaseDelete<TEntity> dto, bool IsAtomicOperation = true)
        {
            return await Handle(async () =>
            {
                await UnitOfWork.ExecuteAsync(async () =>
                {
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<TEntity>> UpdateItemAsync(IDtoBaseUpdate<TEntity> dto, bool IsAtomicOperation = true)
        {
            return await Handle(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {

                }, IsAtomicOperation);
            }, dto);
        }
    }
}
