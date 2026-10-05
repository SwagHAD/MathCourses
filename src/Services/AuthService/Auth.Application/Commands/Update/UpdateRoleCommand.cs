using SharedKernel.Application.Commands;
using Application.Responses.DefaultResponses;

namespace Application.Commands.Update
{
    public sealed record class UpdateRoleCommand : ICommand<RoleResponse>
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public List<int> PermissionIds { get; init; } = [];
    }
}
