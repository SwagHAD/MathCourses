using Application.DTO.StudentDTO;
using Application.Handlers.Base;
using Domain.Entities;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateStudentHandler(IMathDbContext DbContext) : IHandler<Student, UpdateStudentDto>
    {
        public async Task<Student> Handle(UpdateStudentDto dto)
        {
            var student = await DbContext.Set<Student>().FirstOrDefaultAsync(f => f.ID == dto.ID) ?? throw new ArgumentException("Студент не найден");
            student.Name = dto.Name;
            await DbContext.SaveChangesAsync();
            return student;
        }
    }
}
