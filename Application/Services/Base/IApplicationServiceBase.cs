using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity,TDtoBase> where TEntity : BaseEntity 
        where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        Task<Response<TDtoBase>> GetItemAsync(int? ID);
        Task<Response<TDtoBase>> CreateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseCreate<TEntity>;
        Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseDelete<TEntity>;
        Task<Response<TDtoBase>> UpdateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseUpdate<TEntity>;
    }
}
