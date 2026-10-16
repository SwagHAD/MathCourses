using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Teachers;
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
    [Route(MathRoutes.Prefix)]
    public sealed class TeacherController(IMediator mediator) : ControllerBase
    {
        [HttpGet(MathRoutes.Teachers.Get)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<TeacherResponse>> GetTeacher([FromQuery] GetTeacherQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(MathRoutes.Teachers.GetPage)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<TeacherResponse>>> GetTeachers([FromQuery] PageRequest<TeacherFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<TeacherFilter, TeacherResponse>(request), cancellationToken);
        }

        [HttpGet(MathRoutes.Teachers.Count)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfTeachers(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfTeachersQuery(), cancellationToken);
        }

        [HttpPost(MathRoutes.Teachers.Create)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateTeacher([FromBody] CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreateTeacherCommand(request), cancellationToken));
        }

        [HttpPut(MathRoutes.Teachers.Update)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateTeacher([FromBody] UpdateTeacherRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdateTeacherCommand(request), cancellationToken));
        }

        [HttpDelete(MathRoutes.Teachers.Delete)]
        [Permission(ObjectTypeName = nameof(Teacher), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteTeacher([FromQuery] DeleteTeacherCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
