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
    public sealed class CourseController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateCourse(CreateCourseCommand createCourseCommand)
        {
            return Ok(await mediator.Send(createCourseCommand));
        }
        
        [HttpDelete("DeleteCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteCoures(DeleteCourseCommand deleteCourseCommand)
        {
            return Ok(await mediator.Send(deleteCourseCommand));
        }

        [HttpGet("GetCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<DefaultCourseResponse>> GetCourse(GetCourseQuery getCourseCommand)
        {
            return Ok(await mediator.Send(getCourseCommand));
        }

        [HttpPut("UpdateCourse")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateCourse(UpdateCourseCommand updateCourseCommand)
        {
            return Ok(await mediator.Send(updateCourseCommand));
        }

        [HttpGet("GetCountOfCourses")]
        [Permission(ObjectTypeName = nameof(Course), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfCourses(GetCountOfCoursesQuery getCountOfCoursesQuery)
        {
            return await mediator.Send(getCountOfCoursesQuery);
        }
    }
}
