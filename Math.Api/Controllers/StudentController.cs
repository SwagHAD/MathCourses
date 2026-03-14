using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class StudentController(IServiceProvider services) : CrudControllerBase<Student>(services)
    {

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Response<Student>>> CreateStudent(CreateStudentCommand studentDto)
        {
            var result = await CrudService.CreateItemAsync(studentDto);
            return ToActionResult(result);
        }
        [HttpDelete("DeleteStudent")]
        public async Task<ActionResult<Response<Student>>> DeleteStudent(DeleteStudentCommand deletestudentDto)
        {
            var result = await CrudService.DeleteItemAsync(deletestudentDto);
            return ToActionResult(result);
        }
    }
}
