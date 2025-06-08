using Core.Repository;
using DataMath.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Extentions
{
    public static class DIConteinerForServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Group>, GroupRepository>();
            services.AddScoped<IGenericRepository<Student>, StudentRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
