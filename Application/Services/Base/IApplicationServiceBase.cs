using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        Task<Response<TOut>> CreateItemAsync<TDto, TOut>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseCreate<TEntity>;
        Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseDelete<TEntity>;
        Task<Response<TOut>> UpdateItemAsync<TDto, TOut>(TDto dto, bool IsAtomicOperation = true) where TDto : IDTOBaseUpdate<TEntity>;
    }
}
