using MediatR;
using SharedKernel.Application.Commands;
using SharedKernel.Application.Interfaces;

namespace SharedKernel.Application.Behaviors
{
    /// <summary>Оборачивает выполнение команды в транзакцию.</summary>
    public sealed class TransactionBehavior<TRequest, TResponse>(ISwagDbContext dbContext)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await dbContext.BeginTransactionAsync(cancellationToken);
                var response = await next(cancellationToken);
                await dbContext.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch
            {
                await dbContext.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
