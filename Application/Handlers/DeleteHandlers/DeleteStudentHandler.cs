using Application.DTO.StudentDTO;
using Application.Handlers.Base;
using Domain.Entities;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteStudentHandler(IMathDbContext DbContext) : IHandler<Student, DeleteStudentDto>
    {
        public async Task<Student> Handle(DeleteStudentDto dto)
        {
            if(!await DbContext.Set<Student>().AnyAsync(f => f.ID == dto.ID))
                throw new Exception("Student not found");
            await DbContext.Set<Student>().Where(f => f.ID == dto.ID)
                .ExecuteDeleteAsync();
            return default;
        }
    }
}
