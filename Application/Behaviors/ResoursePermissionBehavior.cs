using Application.Interfaces;
using Application.Queries.Base;
using MediatR;

namespace Application.Behaviors
{
    public sealed class ResoursePermissionBehavior<TRequest, TResponse>(ISwagDbContext swagDbContext, IUserProvider userProvider) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId();
            var userType = userProvider.GetUserType();
            if (!await request.CheckResoursePermissionAsync(userId, userType, swagDbContext, cancellationToken))
                throw new UnauthorizedAccessException("У вас нет прав для доступа к этому ресурсу.");
            return await next();
        }
    }
}
