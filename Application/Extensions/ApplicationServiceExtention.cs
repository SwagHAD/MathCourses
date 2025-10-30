using Application.DTO.CourseDTO;
using Application.DTO.GroupDTO;
using Application.DTO.LessonDTO;
using Application.DTO.StudentDTO;
using Application.DTO.TeacherDTO;
using Application.Factory;
using Application.Factory.Base;
using Application.Services.Base;
using Application.Services.UnitOfWork;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddValidation(services);
            AddServices(services);
            return services;
        }
        private static IServiceCollection AddValidation(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorFactoryBase, ValidatorFactory>();
            return services;
        }
        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<Student, StudentDto>, UnitOfWork<Student, StudentDto>>();
            services.AddScoped<IUnitOfWork<Group, GroupDto>, UnitOfWork<Group, GroupDto>>();
            services.AddScoped<IUnitOfWork<Lesson, LessonDto>,  UnitOfWork<Lesson, LessonDto>>();
            services.AddScoped<IUnitOfWork<Teacher, TeacherDto>, UnitOfWork<Teacher, TeacherDto>>();
            services.AddScoped<IUnitOfWork<Course, CourseDto>, UnitOfWork<Course, CourseDto>>();
            services.AddScoped<IApplicationServiceBase<Student, StudentDto>,ApplicationServiceBase<Student, StudentDto>>();
            services.AddScoped<IApplicationServiceBase<Teacher, TeacherDto>, ApplicationServiceBase<Teacher, TeacherDto>>();
            services.AddScoped<IApplicationServiceBase<Lesson, LessonDto>, ApplicationServiceBase<Lesson, LessonDto>>();
            services.AddScoped<IApplicationServiceBase<Group, GroupDto>, ApplicationServiceBase<Group, GroupDto>>();
            services.AddScoped<IApplicationServiceBase<Course, CourseDto>, ApplicationServiceBase<Course, CourseDto>>();
            return services;
        }
    }
}
