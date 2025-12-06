using Domain.Entities.Base;

namespace Application.DTO.Base
{
    public interface IDataTransferObjectBase { }
    public interface IDataTransferObjectBase<T> : IDataTransferObjectBase where T : BaseEntity { }
    public interface IDataTransferObjectBaseCreate<T> : IDataTransferObjectBase<T> where T : BaseEntity { }
    public interface IDataTransferObjectBaseUpdate<T> : IDataTransferObjectBase<T> where T : BaseEntity 
    {
        public int? ID { get; set; }
    }
    public interface IDataTransferObjectBaseDelete<T> : IDataTransferObjectBase<T> where T : BaseEntity 
    {
        public int? ID { get; set; }
    }
    public interface  IDataTransferObjectBaseGet<T> : IDataTransferObjectBase<T> where T : BaseEntity { }
}
