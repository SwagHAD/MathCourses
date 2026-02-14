using Domain.Entities.Base;

namespace Application.DTO.Base
{
    public interface IDtoBase { }
    public interface IDtoBase<T> : IDtoBase where T : BaseEntity { }
    public interface IDtoBaseCreate<T> : IDtoBase<T> where T : BaseEntity { }
    public interface IDtoBaseUpdate<T> : IDtoBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface IDtoBaseDelete<T> : IDtoBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface  IDtoBaseGet<T> : IDtoBase<T> where T : BaseEntity { }
}
