using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application;

namespace Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSharedApplication(typeof(ApplicationConfiguration).Assembly);
            return services;
        }
    }
}
