using Application.DTO.Base;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity, TDtoBase> : IApplicationServiceBase<TEntity, TDtoBase>
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        /// <summary>
        /// Расширение логики создания объекта
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual Task CustomCreate<TDto>(TDto dto) where TDto : IDataTransferObjectBaseCreate<TEntity>
            => Task.CompletedTask;
        /// <summary>
        /// Расширение логики обновления объекта
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual Task CustomUpdate<TDto>(TDto dto) where TDto : IDataTransferObjectBaseUpdate<TEntity>
            => Task.CompletedTask;
        /// <summary>
        /// Расширение логики удаления
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual Task CustomDelete<TDto>(TDto dto) where TDto : IDataTransferObjectBaseDelete<TEntity>
            => Task.CompletedTask;
    }
}
