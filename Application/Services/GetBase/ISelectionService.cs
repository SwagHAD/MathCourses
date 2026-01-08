using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;
using System.Linq.Expressions;

namespace Application.Services.GetBase
{
    public interface ISelectionService<T, TEntity> where T : IDTOBaseGet<TEntity> where TEntity : BaseEntity
    {
        Task<PageListResponse<T>> GetData(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<Response<T>> GetItem(int Id, CancellationToken cancellationToken);
    }
}
