using AuthService.Contracts.Users;
using SharedKernel.Application.Commands;

namespace Application.Commands.Create
{
    public sealed record CreateUserCommand(CreateUserRequest Data) : ICommand<int>;
}
