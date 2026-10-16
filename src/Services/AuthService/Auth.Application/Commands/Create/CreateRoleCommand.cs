using AuthService.Contracts.Roles;
using SharedKernel.Application.Commands;

namespace Application.Commands.Create
{
    public sealed record CreateRoleCommand(CreateRoleRequest Data) : ICommand<int>;
}
