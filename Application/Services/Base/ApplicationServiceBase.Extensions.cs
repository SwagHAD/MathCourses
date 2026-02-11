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
        protected virtual Task<TEntity> CustomCreate(IDtoBaseCreate<TEntity> dto)
        {
            return null;
        }
        /// <summary>
        /// Расширение логики обновления объекта
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual Task<TEntity> CustomUpdate(IDtoBaseUpdate<TEntity> dto)
        {

        }
        /// <summary>
        /// Расширение логики удаления
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual Task CustomDelete(IDtoBaseDelete<TEntity> dto)
        {
            return Task.CompletedTask;
        }
    }
}
