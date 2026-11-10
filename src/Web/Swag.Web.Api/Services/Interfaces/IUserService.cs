using AuthService.Contracts.Users;

namespace Swag.Web.Api.Services.Interfaces
{
    public interface IUserService : ISelectionService<UserResponse, UserResponse, UserFilter>
    {
        /// <summary>Доступы текущего пользователя вида "CoreService:Person:Read:All".</summary>
        Task<IReadOnlySet<string>> GetCurrentPermissionsAsync(CancellationToken cancellationToken = default);
    }
}
