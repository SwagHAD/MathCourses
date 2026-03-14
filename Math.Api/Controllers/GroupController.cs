using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class GroupController(IServiceProvider services) : CrudControllerBase<Group>(services)
    {
        [HttpPost("CreateGroup")]
        public async Task<ActionResult<Response<Group>>> CreateGroup(CreateGroupCommand createGroup)
        {
            var result = await CrudService.CreateItemAsync(createGroup);
            return ToActionResult(result);
        }
        [HttpDelete("DeleteGroup")]
        public async Task<ActionResult<Response<Group>>> DeleteStudent(DeleteGroupCommand deletegroupDto)
        {
            var result = await CrudService.DeleteItemAsync(deletegroupDto);
            return ToActionResult(result);
        }
    }
}
