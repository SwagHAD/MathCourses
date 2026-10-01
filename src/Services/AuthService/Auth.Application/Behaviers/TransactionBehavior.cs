using Application.Commands.Base;
using Application.Interfaces;
using MediatR;

namespace Application.Behaviers
{
    internal sealed class TransactionBehavior<TRequest, TResponse>(IAuthDbContext DbContext) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await DbContext.BeginTransactionAsync(cancellationToken);
                var response = await next();
                await DbContext.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch
            {
                await DbContext.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
