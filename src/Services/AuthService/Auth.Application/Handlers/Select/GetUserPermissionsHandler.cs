using Application.Queries.DefaultQueries;
using MediatR;
using SharedKernel.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class GetUserPermissionsHandler(IPermissionCache permissionCache, IUserProvider userProvider) : IRequestHandler<GetUserPermissionsQuery, IReadOnlySet<string>>
    {
        public async Task<IReadOnlySet<string>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await permissionCache.GetUserPermissionsAsync(userProvider.GetUserId(), cancellationToken);
        }
    }
}
