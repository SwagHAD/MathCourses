using MediatR;

namespace Application.Commands.Base
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
