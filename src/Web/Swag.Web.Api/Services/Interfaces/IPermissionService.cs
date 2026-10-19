using AuthService.Contracts.Permissions;

namespace Swag.Web.Api.Services.Interfaces
{
    public interface IPermissionService : ISelectionService<PermissionResponse, PermissionResponse, PermissionFilter>;
}
