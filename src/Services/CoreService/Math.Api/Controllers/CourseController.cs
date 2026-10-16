using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Courses;
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
    public sealed class CourseController(IMediator mediator) : ControllerBase
    {
        [HttpGet(MathRoutes.Courses.Get)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<CourseResponse>> GetCourse([FromQuery] GetCourseQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(MathRoutes.Courses.GetPage)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<CourseResponse>>> GetCourses([FromQuery] PageRequest<CourseFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<CourseFilter, CourseResponse>(request), cancellationToken);
        }

        [HttpGet(MathRoutes.Courses.Count)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfCourses(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfCoursesQuery(), cancellationToken);
        }

        [HttpPost(MathRoutes.Courses.Create)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateCourse([FromBody] CreateCourseRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreateCourseCommand(request), cancellationToken));
        }

        [HttpPut(MathRoutes.Courses.Update)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateCourse([FromBody] UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdateCourseCommand(request), cancellationToken));
        }

        [HttpDelete(MathRoutes.Courses.Delete)]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteCourse([FromQuery] DeleteCourseCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
