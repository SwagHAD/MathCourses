using Domain.Entities.Base;
using MediatR;

namespace Application.Command.Base
{
    public interface ICommandBase { }
    public interface ICommandBase<T> : ICommandBase where T : BaseEntity { }
    public interface ICommandBaseCreate<T> : IRequest<T>, ICommandBase<T> where T : BaseEntity
    { }
    public interface ICommandBaseUpdate<T> : IRequest<T>, ICommandBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface ICommandBaseDelete<T> : IRequest<T>, ICommandBase<T> where T : BaseEntity 
    {
        public int ID { get; set; }
    }
    public interface  IDtoBaseGet<T> : ICommandBase<T> where T : BaseEntity { }
}
