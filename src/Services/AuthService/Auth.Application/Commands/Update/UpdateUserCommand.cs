using AuthService.Contracts.Users;
using SharedKernel.Application.Commands;

namespace Application.Commands.Update
{
    public sealed record UpdateUserCommand(UpdateUserRequest Data) : ICommand<UserResponse>;
}
