using MediatR;

namespace SharedKernel.Application.Queries
{
    /// <summary>Запрос на чтение данных. Выполняется без транзакции.</summary>
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
