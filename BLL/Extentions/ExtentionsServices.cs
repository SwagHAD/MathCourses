using BLL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extentions
{
    public static class ExtentionsServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
