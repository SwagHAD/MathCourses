using Application.Commands.Auth;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Authenticate(LoginCommand authenticateCommand)
        {
            return await mediator.Send(authenticateCommand);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshResponse>> Refresh(RefreshCommand refreshCommand)
        {
            return await mediator.Send(refreshCommand);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> LogOut(LogoutCommand logOutCommand)
        {
            return await mediator.Send(logOutCommand);
        }
    }
}
