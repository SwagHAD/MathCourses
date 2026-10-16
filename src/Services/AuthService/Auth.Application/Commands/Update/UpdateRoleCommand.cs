using AuthService.Contracts.Roles;
using SharedKernel.Application.Commands;

namespace Application.Commands.Update
{
    public sealed record UpdateRoleCommand(UpdateRoleRequest Data) : ICommand<RoleResponse>;
}
