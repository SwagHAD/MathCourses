using Application.DTO.Base;
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
        public async Task<Response<TOut>> CreateItemAsync<TDto, TOut>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseCreate<TEntity>
        {
            return await Handle<TDto, TOut>(async() =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await CustomCreate(dto);
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseDelete<TEntity>
        {
            return await Handle<TDto>(async () =>
            {
                await UnitOfWork.ExecuteAsync(async () =>
                {
                   await CustomDelete(dto);
                }, IsAtomicOperation);
            }, dto);
        }

        public async Task<Response<TOut>> UpdateItemAsync<TDto, TOut>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseUpdate<TEntity>
        {
            return await Handle<TDto, TOut>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    return await CustomUpdate(dto);
                }, IsAtomicOperation);
            }, dto);
        }
    }
}
