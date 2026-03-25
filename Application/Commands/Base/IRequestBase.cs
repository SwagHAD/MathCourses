using Application.Responses.Base;
using MediatR;

namespace Application.Command.Base
{
    public interface IBaseRequestCreate<T> : IRequest<T>
    { }
    public interface IBaseRequestUpdate<T> : IRequest<T>
    {
        public int ID { get; set; }
    }
    public interface IBaseRequestDelete<T> : IRequest<T>
    {
        public int ID { get; set; }
    }
}
