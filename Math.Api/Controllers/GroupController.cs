using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using Application.Responses;
using Domain.Attributes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Authorize]
    [Route("api/core")]
    public sealed class GroupController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateGroup")]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Create)]
        public async Task<ActionResult<GroupResponse>> CreateGroup(CreateGroupCommand createGroup)
        {
            return await mediator.Send(createGroup);
        }

        [HttpDelete("DeleteGroup")]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Delete)]
        public async Task<ActionResult<DefaultGroupResponse>> DeleteStudent(DeleteGroupCommand deletegroupDto)
        {
            return await mediator.Send(deletegroupDto);
        }

        [HttpGet("GetGroup")]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Read)]
        public async Task<ActionResult<GroupResponse>> GetGroup(GetGroupQuery getGroupCommand)
        {
            return await mediator.Send(getGroupCommand);
        }

        [HttpPut("UpdateGroup")]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Update)]
        public async Task<ActionResult<GroupResponse>> UpdateGroup(UpdateGroupCommand updateGroupCommand)
        {
            return await mediator.Send(updateGroupCommand);
        }
    }
}
