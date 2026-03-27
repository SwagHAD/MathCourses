using Application.Base;
using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Enums;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class CourseController(ICrudServiceBase<Course> CourseService) : ControllerBase
    {
        [HttpPost("CreateCourse")]
        public async Task<ActionResult<Response<DefaultCourseResponse>>> CreateCourse(CreateCourseCommand createCourseCommand)
        {
            var result = await CourseService.CreateItemAsync<CreateCourseCommand, DefaultCourseResponse>(createCourseCommand);
            return result.Status switch
            { 
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
        [HttpDelete("DeleteCourse")]
        public async Task<ActionResult<Response<DefaultCourseResponse>>> DeleteCoures(DeleteCourseCommand deleteCourseCommand)
        {
            var result = await CourseService.DeleteItemAsync<DeleteCourseCommand, DefaultCourseResponse>(deleteCourseCommand);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result),
            };
        }
    }
}
