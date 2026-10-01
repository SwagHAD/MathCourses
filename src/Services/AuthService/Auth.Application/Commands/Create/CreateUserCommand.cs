using Application.Commands.Base;

namespace Application.Commands.Create
{
    public sealed record CreateUserCommand : ICommand<int>
    {
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
