using Application.Services.Base;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationServiceBase<Student>, ApplicationServiceBase<Student>>();
            return services;
        }
    }
}
