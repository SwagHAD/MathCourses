using Application.DTO.CourseDTO;
using Application.DTO.GroupDTO;
using Application.DTO.LessonDTO;
using Application.DTO.StudentDTO;
using Application.DTO.TeacherDTO;
using Application.Factory;
using Application.Factory.Base;
using Application.Handlers.Base;
using Application.Handlers.CreateHandlers;
using Application.Handlers.DeleteHandlers;
using Application.Handlers.UpdateHandlers;
using Application.Mapping.Base;
using Application.Services.Base;
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
            AddMapping(services);
            AddServices(services);
            AddHandlers(services);
            return services;
        }
        private static void AddValidation(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorFactoryBase, ValidatorFactory>();
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IApplicationServiceBase<Student, StudentDto>,ApplicationServiceBase<Student, StudentDto>>();
            services.AddScoped<IApplicationServiceBase<Teacher, TeacherDto>, ApplicationServiceBase<Teacher, TeacherDto>>();
            services.AddScoped<IApplicationServiceBase<Lesson, LessonDto>, ApplicationServiceBase<Lesson, LessonDto>>();
            services.AddScoped<IApplicationServiceBase<Group, GroupDto>, ApplicationServiceBase<Group, GroupDto>>();
            services.AddScoped<IApplicationServiceBase<Course, CourseDto>, ApplicationServiceBase<Course, CourseDto>>();
        }
        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IHandler<Student, CreateStudentDto>, CreateStudentHandler>();
            services.AddScoped<IHandler<Student, DeleteStudentDto>, DeleteStudentHandler>();
            services.AddScoped<IHandler<Student, UpdateStudentDto>, UpdateStudentHandler>();
        }
        private static void AddMapping(IServiceCollection services)
        {
            services.AddAutoMapper(ctg => { } ,typeof(AssemblyMappingProfile));
        }
    }
}
