using Application.Behaviors;
using Application.Mapping.Base;
using Application.Services.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
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
            AddMediatR(services);
            return services;
        }
        private static void AddValidation(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICrudServiceBase<Student>, CrudServiceBase<Student>>();
            services.AddScoped<ICrudServiceBase<Teacher>, CrudServiceBase<Teacher>>();
            services.AddScoped<ICrudServiceBase<Lesson>, CrudServiceBase<Lesson>>();
            services.AddScoped<ICrudServiceBase<Group>, CrudServiceBase<Group>>();
            services.AddScoped<ICrudServiceBase<Course>, CrudServiceBase<Course>>();
        }
        private static void AddMapping(IServiceCollection services)
        {
            services.AddAutoMapper(ctg => { } ,typeof(AssemblyMappingProfile));
        }
        private static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
