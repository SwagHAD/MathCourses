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
    public sealed class TeacherController(ICrudServiceBase<Teacher> TeacherService) : ControllerBase
    {
        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<Response<DefaultTeacherResponse>>> CreateTeacher(CreateTeacherCommand teacherCommand)
        {
            var result = await TeacherService.ExecuteAsync<CreateTeacherCommand, DefaultTeacherResponse>(teacherCommand);
            return result.Status switch
            { 
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
        [HttpDelete("DeleteTeacher")]
        public async Task<ActionResult<Response<DefaultTeacherResponse>>> DeleteTeacher(DeleteTeacherCommand deleteTeacherCommand)
        {
            var result = await TeacherService.ExecuteAsync<DeleteTeacherCommand, DefaultTeacherResponse>(deleteTeacherCommand);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
