using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
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
    public sealed class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Create)]
        public async Task<ActionResult<DefaultStudentResponse>> CreateStudent(CreateStudentCommand studentDto)
        {
            return await mediator.Send(studentDto);
        }

        [HttpDelete("DeleteStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Delete)]
        public async Task<ActionResult<DefaultStudentResponse>> DeleteStudent(DeleteStudentCommand deletestudentDto)
        {
            return await mediator.Send(deletestudentDto);
        }

        [HttpGet("GetStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultStudentResponse>> GetStudent(GetStudentQuery getStudentCommand)
        {
            return await mediator.Send(getStudentCommand);
        }
    }
}
