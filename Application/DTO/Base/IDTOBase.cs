using Domain.Entities.Base;

namespace Application.DTO.Base
{
    public interface IDTOBase { }
    public interface IDTOBase<T> : IDTOBase where T : BaseEntity { }
    public interface IDTOBaseCreate<T> : IDTOBase<T> where T : BaseEntity { }
    public interface IDTOBaseUpdate<T> : IDTOBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface IDTOBaseDelete<T> : IDTOBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface  IDTOBaseGet<T> : IDTOBase<T> where T : BaseEntity { }
}
