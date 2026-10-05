using SharedKernel.Application.Commands;
using Application.Responses.DefaultResponses;

namespace Application.Commands.Update
{
    public sealed record UpdateUserCommand : ICommand<UserResponse>
    {
        public int Id { get; init; }
        public string Login { get; init; } = null!;
        public string PasswordHash { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
