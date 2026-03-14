using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class CourseController(IServiceProvider services) : CrudControllerBase<Course>(services)
    {
        [HttpPost("CreateCourse")]
        public async Task<ActionResult<Response<Course>>> CreateCourse(CreateCourseCommand createCourseCommand)
        {
            var result = await CrudService.CreateItemAsync(createCourseCommand);
            return ToActionResult(result);
        }
        [HttpDelete("DeleteCourse")]
        public async Task<ActionResult<Response<Course>>> DeleteCoures(DeleteCourseCommand deleteCourseCommand)
        {
            var result = await CrudService.DeleteItemAsync(deleteCourseCommand);
            return ToActionResult(result);
        }
    }
}
