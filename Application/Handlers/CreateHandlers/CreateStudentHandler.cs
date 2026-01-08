using Application.DTO.StudentDTO;
using Application.Handlers.Base;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateStudentHandler(IMathDbContext DbContext, IMapper Mapper) : IHandler<Student, CreateStudentDto>
    {
        public async Task<Student> Handle(CreateStudentDto dto)
        {
            var student = Mapper.Map<Student>(dto);
            await DbContext.AddAsync(student);
            await DbContext.SaveChangesAsync();
            return student;
        }
    }
}
