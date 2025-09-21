using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public interface IApplicationServiceBase<T> where T : BaseEntity
    {
        Task<Response<IDataTransferObjectBase<T>>> Create(IDataTransferObjectBase<T> DTO);
        Task<Response<IDataTransferObjectBase<T>>> Update(IDataTransferObjectBase<T> DTO);
        Task<Response<IDataTransferObjectBase<T>>> Delete(IDataTransferObjectBase<T> DTO);
    }
}
