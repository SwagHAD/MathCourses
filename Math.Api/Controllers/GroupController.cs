using Application.Base;
using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Enums;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class GroupController(ICrudServiceBase<Group> GroupService) : ControllerBase
    {
        [HttpPost("CreateGroup")]
        public async Task<ActionResult<Response<DefaultGroupResponse>>> CreateGroup(CreateGroupCommand createGroup)
        {
            var result = await GroupService.CreateItemAsync<CreateGroupCommand, GroupResponse>(createGroup);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
        [HttpDelete("DeleteGroup")]
        public async Task<ActionResult<Response<DefaultGroupResponse>>> DeleteStudent(DeleteGroupCommand deletegroupDto)
        {
            var result = await GroupService.DeleteItemAsync<DeleteGroupCommand, DefaultGroupResponse>(deletegroupDto);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
