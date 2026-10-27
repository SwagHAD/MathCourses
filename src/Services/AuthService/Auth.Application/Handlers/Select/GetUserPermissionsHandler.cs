using Application.Queries.DefaultQueries;
using MediatR;
using SharedKernel.Cache;
using SharedKernel.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class GetUserPermissionsHandler(IPermissionCacheService permissionCache) : IRequestHandler<GetUserPermissionsQuery, IReadOnlySet<string>>
    {
        public async Task<IReadOnlySet<string>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await permissionCache.GetCurrentUserPermissionsAsync(cancellationToken);
        }
    }
}
