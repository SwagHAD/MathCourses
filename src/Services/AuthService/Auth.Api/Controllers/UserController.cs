using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using SharedKernel.Attributes;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public sealed class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("GetUser")]
        [Permission(ObjectTypeName = nameof(User), ActionType = ActionType.Read)]
        public async Task<ActionResult<UserResponse>> GetUser(GetUserQuery getUserCommand)
        {
            return await mediator.Send(getUserCommand);
        }

        [HttpGet("GetUserPermissions")]
        public async Task<ActionResult<HashSet<string>>> GetPermissions(GetUserPermissionsQuery getUserPermissions)
        {
            var response = await mediator.Send(getUserPermissions);
            return Ok(response);
        }
    }
}
