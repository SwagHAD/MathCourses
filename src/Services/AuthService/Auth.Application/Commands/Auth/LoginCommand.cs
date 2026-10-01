using Application.Commands.Base;
using Application.Responses;

namespace Application.Commands.Auth
{
    public sealed record LoginCommand : ICommand<LoginResponse>
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
