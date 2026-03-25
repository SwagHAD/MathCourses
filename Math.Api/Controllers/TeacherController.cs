using Application.Base;
using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class TeacherController(IServiceProvider services) : CrudControllerBase<Teacher>(services)
    {
        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<Response<Teacher>>> CreateTeacher(CreateTeacherCommand teacherCommand)
        {
            var result = await CrudService.CreateItemAsync(teacherCommand);
            return ToActionResult(result);
        }
        [HttpDelete("DeleteTeacher")]
        public async Task<ActionResult<Response<Teacher>>> DeleteTeacher(DeleteTeacherCommand deleteTeacherCommand)
        {
            var result = await CrudService.DeleteItemAsync(deleteTeacherCommand);
            return ToActionResult(result);
        }
    }
}
