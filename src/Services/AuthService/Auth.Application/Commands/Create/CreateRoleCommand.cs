using SharedKernel.Application.Commands;

namespace Application.Commands.Create
{
    public sealed record CreateRoleCommand : ICommand<int>
    {
        public string Name { get; init; } = null!;

        public List<int> PermissionIds { get; init; } = [];
    }
}
