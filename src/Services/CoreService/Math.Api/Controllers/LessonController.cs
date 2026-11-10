using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Lessons;
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
    public sealed class LessonController(IMediator mediator) : ControllerBase
    {
        [HttpGet(CoreRoutes.Lessons.Get)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Read)]
        public async Task<ActionResult<LessonResponse>> GetLesson([FromQuery] GetLessonQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(CoreRoutes.Lessons.GetPage)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<LessonResponse>>> GetLessons([FromQuery] PageRequest<LessonFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<LessonFilter, LessonResponse>(request), cancellationToken);
        }

        [HttpGet(CoreRoutes.Lessons.Count)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfLessons(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfLessonsQuery(), cancellationToken);
        }

        [HttpPost(CoreRoutes.Lessons.Create)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateLesson([FromBody] CreateLessonRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreateLessonCommand(request), cancellationToken));
        }

        [HttpPut(CoreRoutes.Lessons.Update)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateLesson([FromBody] UpdateLessonRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdateLessonCommand(request), cancellationToken));
        }

        [HttpDelete(CoreRoutes.Lessons.Delete)]
        [Permission(ObjectTypeName = nameof(Lesson), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteLesson([FromQuery] DeleteLessonCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
