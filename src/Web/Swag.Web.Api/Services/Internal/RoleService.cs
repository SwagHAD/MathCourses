using AuthService.Contracts;
using AuthService.Contracts.Roles;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class RoleService(HttpClient httpClient)
        : SelectionService<RoleResponse, RoleResponse, RoleFilter>(httpClient, Urls), IRoleService
    {
        private static readonly EntityRoutes Urls = new(
            Get: AuthRoutes.Url(AuthRoutes.Roles.Get),
            GetPage: AuthRoutes.Url(AuthRoutes.Roles.GetPage),
            Create: AuthRoutes.Url(AuthRoutes.Roles.Create),
            Update: AuthRoutes.Url(AuthRoutes.Roles.Update),
            Delete: AuthRoutes.Url(AuthRoutes.Roles.Delete));

        public Task<int> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default) =>
            PostJsonAsync<CreateRoleRequest, int>(Urls.Create, request, cancellationToken);

        public Task<RoleResponse> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default) =>
            PutJsonAsync<UpdateRoleRequest, RoleResponse>(Urls.Update, request, cancellationToken);

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
            SendDeleteAsync(WithId(Urls.Delete, id), cancellationToken);
    }
}
