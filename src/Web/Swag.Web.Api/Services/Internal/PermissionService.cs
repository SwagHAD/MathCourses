using AuthService.Contracts;
using AuthService.Contracts.Permissions;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class PermissionService(HttpClient httpClient)
        : SelectionService<PermissionResponse, PermissionResponse, PermissionFilter>(httpClient, Urls), IPermissionService
    {
        private static readonly EntityRoutes Urls = new(
            Get: AuthRoutes.Url(AuthRoutes.Permissions.Get),
            GetPage: AuthRoutes.Url(AuthRoutes.Permissions.GetPage));
    }
}
