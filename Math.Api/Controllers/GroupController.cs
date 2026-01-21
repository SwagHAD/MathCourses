using Application.DTO.GroupDTO;
using Application.Enums;
using Application.Services.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class GroupController(IApplicationServiceBase<Group> GroupService) : ControllerBase
    {
        [HttpPost("CreateGroup")]
        public async Task<ActionResult<GroupDto>> CreateGroup(CreateGroupDto createGroup)
        {
            var result = await GroupService.CreateItemAsync<CreateGroupDto, GroupDto>(createGroup);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
