using Domain.Entities.Base;

namespace Application.DTO.Base
{
    public interface IDataTransferObjectBase { }
    public interface IDataTransferObjectBase<T> : IDataTransferObjectBase where T : BaseEntity { }
}
