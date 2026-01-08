using Application.DTO.TeacherDTO;
using Application.Handlers.Base;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateTeacherHandler(IMapper Mapper, IMathDbContext DbContext) : IHandler<Teacher, CreateTeacherDto>
    {
        public async Task<Teacher> Handle(CreateTeacherDto dto)
        {
            var teacher = Mapper.Map<Teacher>(dto);
            await DbContext.AddAsync(teacher);
            await DbContext.SaveChangesAsync();
            return teacher;
        }
    }
}
