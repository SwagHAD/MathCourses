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
            if(!await DbContext.Set<Student>().AnyAsync(s => s.ID == dto.ID))
            {
                throw new InvalidOperationException($"Student with ID {dto.ID} not found.");
            }
            await DbContext.Set<Student>().Where(f => f.ID == dto.ID)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(f => f.Name, dto.Name));
            return await DbContext.Set<Student>().AsNoTracking().FirstOrDefaultAsync(f => f.ID == dto.ID);
        }
    }
}
