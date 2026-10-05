using MediatR;

namespace SharedKernel.Application.Commands
{
    /// <summary>Команда, изменяющая данные. Выполняется в транзакции (см. TransactionBehavior).</summary>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
