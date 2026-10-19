using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Students;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Pagination;
using SharedKernel.Attributes;
using SharedKernel.Contracts.Pagination;
using SharedKernel.Enums;

namespace Math.Api.Controllers
{
    [Authorize]
    [Route(CoreRoutes.Prefix)]
    public sealed class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpGet(CoreRoutes.Students.Get)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<StudentResponse>> GetStudent([FromQuery] GetStudentQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(CoreRoutes.Students.GetPage)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<StudentResponse>>> GetStudents([FromQuery] PageRequest<StudentFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<StudentFilter, StudentResponse>(request), cancellationToken);
        }

        [HttpGet(CoreRoutes.Students.Count)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfStudents(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfStudentsQuery(), cancellationToken);
        }

        [HttpPost(CoreRoutes.Students.Create)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateStudent([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreateStudentCommand(request), cancellationToken));
        }

        [HttpPut(CoreRoutes.Students.Update)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdateStudentCommand(request), cancellationToken));
        }

        [HttpDelete(CoreRoutes.Students.Delete)]
        [Permission(ObjectTypeName = nameof(Student), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteStudent([FromQuery] DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
