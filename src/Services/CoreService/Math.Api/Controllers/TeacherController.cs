using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using Application.Responses;
using SharedKernel.Attributes;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;

namespace Math.Api.Controllers
{
    [Authorize]
    [Route("api/core")]
    public sealed class TeacherController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateTeacher(CreateTeacherCommand teacherCommand)
        {
            return Ok(await mediator.Send(teacherCommand));
        }
        [HttpDelete("DeleteTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteTeacher(DeleteTeacherCommand deleteTeacherCommand)
        {
            return Ok(await mediator.Send(deleteTeacherCommand));
        }
        [HttpGet("GetTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultTeacherResponse>> GetTeacher(GetTeacherQuery getTeacherCommand)
        {
            return await mediator.Send(getTeacherCommand);
        }
        [HttpPut("UpdateTeacher")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateTeacher(UpdateTeacherCommand updateTeacherCommand)
        {
            return Ok(await mediator.Send(updateTeacherCommand));
        }

        [HttpGet("GetCountOfTeachers")]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfTeachers(GetCountOfTeachersQuery getCountOfTeachersQuery)
        {
            return await mediator.Send(getCountOfTeachersQuery);
        }
    }
}
