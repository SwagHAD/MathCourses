using Application.DTO.CourseDTO;
using Application.Handlers.Base;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateCourseHandler(IMapper mapper, IMathDbContext DbContext) : IHandler<Course, CreateCourseDto>
    {
        public async Task<Course> Handle(CreateCourseDto dto)
        {
            var newcourse = mapper.Map<Course>(dto);
            await DbContext.Courses.AddAsync(newcourse);
            await DbContext.SaveChangesAsync();
            return newcourse;
        }
    }
}
