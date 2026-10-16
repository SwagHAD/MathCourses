using AuthService.Contracts.Auth;
using SharedKernel.Application.Commands;

namespace Application.Commands.Auth
{
    public sealed record RefreshCommand(RefreshTokenRequest Data) : ICommand<TokenResponse>;
}
