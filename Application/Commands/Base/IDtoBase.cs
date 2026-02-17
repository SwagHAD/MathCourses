using Domain.Entities.Base;
using MediatR;

namespace Application.Command.Base
{
    public interface IDtoBase { }
    public interface IDtoBase<T> : IDtoBase where T : BaseEntity { }
    public interface IDtoBaseCreate<T> : IRequest<T>, IDtoBase<T> where T : BaseEntity
    { }
    public interface IDtoBaseUpdate<T> : IRequest<T>, IDtoBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface IDtoBaseDelete<T> : IRequest, IDtoBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface  IDtoBaseGet<T> : IDtoBase<T> where T : BaseEntity { }
}
