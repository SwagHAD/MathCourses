using Domain.Entities.Base;
using MediatR;

namespace Application.Command.Base
{
    public interface ICommandBase { }
    public interface ICommadBase<T> : ICommandBase where T : BaseEntity { }
    public interface ICommandBaseCreate<T> : IRequest<T>, ICommadBase<T> where T : BaseEntity
    { }
    public interface ICommandBaseUpdate<T> : IRequest<T>, ICommadBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface ICommandBaseDelete<T> : IRequest, ICommadBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface  IDtoBaseGet<T> : ICommadBase<T> where T : BaseEntity { }
}
