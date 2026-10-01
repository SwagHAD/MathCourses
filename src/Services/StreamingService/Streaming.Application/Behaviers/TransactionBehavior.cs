using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamingService.Application.Commands.Base;
using StreamingService.Application.Common.Interfaces;

namespace StreamingService.Application.Behaviers
{
    internal sealed class TransactionBehavior<TRequest, TResponse>(IStreamDbContext streamDbContext) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                await streamDbContext.BeginTransactionAsync(cancellationToken);
                var response = await next();
                await streamDbContext.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch
            {
                await streamDbContext.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
