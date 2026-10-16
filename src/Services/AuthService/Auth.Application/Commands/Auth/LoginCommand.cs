using AuthService.Contracts.Auth;
using SharedKernel.Application.Commands;

namespace Application.Commands.Auth
{
    public sealed record LoginCommand(LoginRequest Data) : ICommand<TokenResponse>;
}
