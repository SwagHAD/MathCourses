using Application.Commands.Create;
using Application.Commands.Delete;
using Application.Commands.Update;
using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using Domain.Attributes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public sealed class RoleController(IMediator mediator) : ControllerBase
    {
        [HttpGet("GetRole")]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Read)]
        public async Task<ActionResult<RoleResponse>> GetRole(GetRoleQuery getRoleCommand)
        {
            return await mediator.Send(getRoleCommand);
        }
        
        [HttpPost("CreateRole")]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Create)]
        public async Task<ActionResult<int>> CreateRole(CreateRoleCommand createRoleCommand)
        {
            return await mediator.Send(createRoleCommand);
        }

        [HttpPut("UpdateRole")]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Update)]
        public async Task<ActionResult<RoleResponse>> UpdateRole(UpdateRoleCommand updateRoleCommand)
        {
            return await mediator.Send(updateRoleCommand);
        }

        [HttpDelete("DeleteRole")]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Delete)]
        public async Task<ActionResult<Unit>> DeleteRole(DeleteRoleCommand deleteRoleCommand)
        {
            return await mediator.Send(deleteRoleCommand);
        }
    }
}
