using MediatR;

namespace Application.Queries.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
