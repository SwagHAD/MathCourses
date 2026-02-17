using Application.Factory;
using Application.Factory.Base;
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
