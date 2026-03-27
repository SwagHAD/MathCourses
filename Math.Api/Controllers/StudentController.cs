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
    public sealed class StudentController(ICrudServiceBase<Student> StudentService) : ControllerBase
    {

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Response<DefaultStudentResponse>>> CreateStudent(CreateStudentCommand studentDto)
        {
            var result = await StudentService.CreateItemAsync<CreateStudentCommand, DefaultStudentResponse>(studentDto);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
        [HttpDelete("DeleteStudent")]
        public async Task<ActionResult<Response<DefaultStudentResponse>>> DeleteStudent(DeleteStudentCommand deletestudentDto)
        {
            var result = await StudentService.DeleteItemAsync<DeleteStudentCommand, DefaultStudentResponse>(deletestudentDto);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
