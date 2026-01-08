using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity,TDtoBase> where TEntity : BaseEntity 
        where TDtoBase : IDTOBase<TEntity>
    {
        Task<Response<TDtoBase>> CreateItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseCreate<TEntity>;
        Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseDelete<TEntity>;
        Task<Response<TDtoBase>> UpdateItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseUpdate<TEntity>;
    }
}
