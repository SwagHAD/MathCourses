using AuthService.Contracts;
using AuthService.Contracts.Users;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class UserService(HttpClient httpClient)
        : SelectionService<UserResponse, UserResponse, UserFilter>(httpClient, Urls), IUserService
    {
        private static readonly EntityRoutes Urls = new(
            Get: AuthRoutes.Url(AuthRoutes.Users.Get),
            GetPage: AuthRoutes.Url(AuthRoutes.Users.GetPage));

        public async Task<IReadOnlySet<string>> GetCurrentPermissionsAsync(CancellationToken cancellationToken = default) =>
            await GetJsonAsync<HashSet<string>>(AuthRoutes.Url(AuthRoutes.Users.CurrentPermissions), cancellationToken);
    }
}
