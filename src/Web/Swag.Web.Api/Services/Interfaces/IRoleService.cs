using AuthService.Contracts.Roles;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>У ролей сервер возвращает результат записи, поэтому методы свои, а не из ICrudService.</summary>
    public interface IRoleService : ISelectionService<RoleResponse, RoleResponse, RoleFilter>
    {
        /// <returns>Id созданной роли.</returns>
        Task<int> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
        Task<RoleResponse> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
