using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using Application.Queries.PaginationQueries;
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
        public async Task<ActionResult> CreateStudent(CreateStudentCommand studentDto)
        {
            return Ok(await mediator.Send(studentDto));
        }

        [HttpDelete("DeleteStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteStudent(DeleteStudentCommand deletestudentDto)
        {
            return Ok(await mediator.Send(deletestudentDto));
        }

        [HttpPut("UpdateStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateStudent(UpdateStudentCommand updateCommand)
        {
            return Ok(await mediator.Send(updateCommand));
        }

        [HttpGet("GetStudent")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultStudentResponse>> GetStudent(GetStudentQuery getStudentCommand)
        {
            return await mediator.Send(getStudentCommand);
        }

        [HttpGet("GetAll")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultStudentResponse[]>> GetAllStudents(GetStudentsPaginationQuery query)
        
        {
            return await mediator.Send(query);
        }

        [HttpGet("GetCountOfStudents")]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfStudents(GetCountOfStudentsQuery getCountOfStudentsQuery)
        {
            return await mediator.Send(getCountOfStudentsQuery);
        }
    }
}
