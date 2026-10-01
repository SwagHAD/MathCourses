using MediatR;

namespace StreamingService.Application.Commands.Base
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
