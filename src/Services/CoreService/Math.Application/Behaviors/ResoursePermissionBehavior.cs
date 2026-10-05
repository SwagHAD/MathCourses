using Application.Queries.Base;
using MediatR;
using SharedKernel.Interfaces;
using SharedKernel.Application.Interfaces;

namespace Application.Behaviors
{
    public sealed class ResoursePermissionBehavior<TRequest, TResponse>(ISwagDbContext swagDbContext, IUserProvider userProvider) 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IResoursePermissionQuery<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId();
            var roleType = userProvider.GetRoleType();
            if (!await request.CheckResoursePermissionAsync(userId, roleType, swagDbContext, cancellationToken))
                throw new UnauthorizedAccessException("У вас нет прав для доступа к этому ресурсу.");
            return await next();
        }
    }
}
