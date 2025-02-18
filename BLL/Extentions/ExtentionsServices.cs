using BLL.Repository;
using DataMath.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extentions
{
    public static class ExtentionsServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Group>, GroupRepository>();
            return services;
        }
    }
}
