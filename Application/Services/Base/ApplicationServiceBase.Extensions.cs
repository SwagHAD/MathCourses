using Application.DTO.Base;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity> : IApplicationServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Расширение логики создания объекта
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected async Task<TEntity> CustomCreate<TDto>(TDto dto) where TDto : IDTOBaseCreate<TEntity>
        {
            var service = HandlerFactory.GetHandle<TDto>();
            return await service.Handle(dto);
        }
        /// <summary>
        /// Расширение логики обновления объекта
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected async Task<TEntity> CustomUpdate<TDto>(TDto dto) where TDto : IDTOBaseUpdate<TEntity>
        {
            var service = HandlerFactory.GetHandle<TDto>();
            return await service.Handle(dto);
        }
        /// <summary>
        /// Расширение логики удаления
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected async Task CustomDelete<TDto>(TDto dto) where TDto : IDTOBaseDelete<TEntity>
        {
            var service = HandlerFactory.GetHandle<TDto>();
            await service.Handle(dto);
        }
    }
}
