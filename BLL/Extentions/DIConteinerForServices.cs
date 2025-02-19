using BLL.Mapping;
using BLL.Repository;
using DataMath.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BLL.Extentions
{
    public static class DIConteinerForServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Group>, GroupRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
