using Application.Commands.Base;
using Application.Responses;

namespace Application.Commands.Auth
{
    public sealed record RefreshCommand : ICommand<RefreshResponse>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
