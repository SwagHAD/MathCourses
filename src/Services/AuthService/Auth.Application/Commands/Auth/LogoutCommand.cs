using SharedKernel.Application.Commands;
using Application.Responses;

namespace Application.Commands.Auth
{
    public sealed record LogoutCommand : ICommand<LogoutResponse>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
