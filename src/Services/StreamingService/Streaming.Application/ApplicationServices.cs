using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application;

namespace StreamingService.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSharedApplication(typeof(ApplicationServices).Assembly);
            return services;
        }
    }
}
