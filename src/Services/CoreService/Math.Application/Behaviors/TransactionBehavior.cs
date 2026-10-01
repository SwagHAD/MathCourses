using Application.Commands.Base;
using Application.Interfaces;
using MediatR;

namespace Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>(ISwagDbContext swagContext) : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await swagContext.BeginTransactionAsync(cancellationToken);
                var response = await next();
                await swagContext.CommitTransactionAsync(cancellationToken);
                return response;

            }
            catch
            {
                await swagContext.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
