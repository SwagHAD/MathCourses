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
    public sealed class TeacherController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Create)]
        public async Task<ActionResult<DefaultTeacherResponse>> CreateTeacher(CreateTeacherCommand teacherCommand)
        {
            return await mediator.Send(teacherCommand);
        }
        [HttpDelete("DeleteTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Delete)]
        public async Task<ActionResult<DefaultTeacherResponse>> DeleteTeacher(DeleteTeacherCommand deleteTeacherCommand)
        {
            return await mediator.Send(deleteTeacherCommand);
        }
        [HttpGet("GetTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultTeacherResponse>> GetTeacher(GetTeacherQuery getTeacherCommand)
        {
            return await mediator.Send(getTeacherCommand);
        }
        [HttpPut("UpdateTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Update)]
        public async Task<ActionResult<DefaultTeacherResponse>> UpdateTeacher(UpdateTeacherCommand updateTeacherCommand)
        {
            return await mediator.Send(updateTeacherCommand);
        }
    }
}
