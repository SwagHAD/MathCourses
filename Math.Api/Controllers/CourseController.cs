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
    public sealed class CourseController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Create)]
        public async Task<ActionResult<DefaultCourseResponse>> CreateCourse(CreateCourseCommand createCourseCommand)
        {
            return await mediator.Send(createCourseCommand);
        }
        
        [HttpDelete("DeleteCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Delete)]
        public async Task<ActionResult<DefaultCourseResponse>> DeleteCoures(DeleteCourseCommand deleteCourseCommand)
        {
            return await mediator.Send(deleteCourseCommand);
        }

        [HttpGet("GetCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultCourseResponse>> GetCourse(GetCourseQuery getCourseCommand)
        {
            return await mediator.Send(getCourseCommand);
        }

        [HttpPut("UpdateCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Update)]
        public async Task<ActionResult<DefaultCourseResponse>> UpdateCourse(UpdateCourseCommand updateCourseCommand)
        {
            return await mediator.Send(updateCourseCommand);
        }
    }
}
