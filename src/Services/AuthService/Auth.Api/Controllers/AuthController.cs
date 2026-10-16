using Application.Commands.Auth;
using AuthService.Contracts;
using AuthService.Contracts.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route(AuthRoutes.Prefix)]
    public sealed class AuthController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost(AuthRoutes.Auth.Login)]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new LoginCommand(request), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost(AuthRoutes.Auth.Refresh)]
        public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new RefreshCommand(request), cancellationToken);
        }

        [HttpPost(AuthRoutes.Auth.Logout)]
        public async Task<ActionResult<LogoutResponse>> Logout([FromBody] LogoutRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new LogoutCommand(request), cancellationToken);
        }
    }
}
