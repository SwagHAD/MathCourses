using Application.DTO.StudentDTO;
using Application.Enums;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities;
using Domain.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class StudentController(IApplicationServiceBase<Student, StudentDto> StudentService) : ControllerBase
    {
        //[HttpGet("GetStudentById")]
        //public async Task<Response<StudentDto>> GetStudentById(int Id)
        //{
        //    return Ok();
        //}

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Response<StudentDto>>> CreateStudent(CreateStudentDto studentDto)
        {
            var result = await StudentService.CreateItemAsync(studentDto);
            return result.Status switch
            {
                ResponseStatus.Ok => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}
