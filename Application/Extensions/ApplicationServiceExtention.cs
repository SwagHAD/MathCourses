using Application.Commands;
using Application.DTO.CourseDTO;
using Application.DTO.GroupDTO;
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
            services.AddScoped<IApplicationServiceBase<Student>, ApplicationServiceBase<Student>>();
            services.AddScoped<IApplicationServiceBase<Teacher>, ApplicationServiceBase<Teacher>>();
            services.AddScoped<IApplicationServiceBase<Lesson>, ApplicationServiceBase<Lesson>>();
            services.AddScoped<IApplicationServiceBase<Group>, ApplicationServiceBase<Group>>();
            services.AddScoped<IApplicationServiceBase<Course>, ApplicationServiceBase<Course>>();
        }
        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IHandler<Student>, CreateStudentHandler>();
            services.AddScoped<IHandler<Student>, DeleteStudentHandler>();
            services.AddScoped<IHandler<Student>, UpdateStudentHandler>();
            services.AddScoped<IHandler<Teacher, CreateTeacherDto>, CreateTeacherHandler>();
            services.AddScoped<IHandler<Group, CreateGroupDto>, CreateGroupHandler>();
            services.AddScoped<IHandler<Course, CreateCourseDto>, CreateCourseHandler>();
        }
        private static void AddMapping(IServiceCollection services)
        {
            services.AddAutoMapper(ctg => { } ,typeof(AssemblyMappingProfile));
        }
        private static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyMediatoRDI).Assembly));
        }
    }
}
