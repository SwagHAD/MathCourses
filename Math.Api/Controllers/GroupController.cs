using Application.Commands.CreateCommands;
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
        public async Task<ActionResult<Group>> CreateGroup(CreateGroupCommand createGroup)
        {
            var result = await GroupService.CreateItemAsync(createGroup);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
