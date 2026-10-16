using AuthService.Contracts.Auth;
using SharedKernel.Application.Commands;

namespace Application.Commands.Auth
{
    public sealed record LogoutCommand(LogoutRequest Data) : ICommand<LogoutResponse>;
}
