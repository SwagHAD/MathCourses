using Application.Behaviors;
using Application.Mapping.Base;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application;

namespace Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSharedApplication(typeof(ApplicationServices).Assembly,
                cfg => cfg.AddOpenBehavior(typeof(ResoursePermissionBehavior<,>)));
            AddMapping(services);
            return services;
        }
        private static void AddMapping(IServiceCollection services)
        {
            services.AddAutoMapper(ctg => { } ,typeof(AssemblyMappingProfile));
        }
    }
}
