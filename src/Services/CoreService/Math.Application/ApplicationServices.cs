using Application.Access;
using Application.Behaviors;
using Application.Mapping;
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
            AddAccessProjections(services);
            return services;
        }

        /// <summary>Все проекции кортежей сборки — без состояния, поэтому singleton.</summary>
        private static void AddAccessProjections(IServiceCollection services)
        {
            var projections = typeof(ApplicationServices).Assembly.GetTypes()
                .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IAccessTupleProjection).IsAssignableFrom(type));
            foreach (var projection in projections)
                services.AddSingleton(typeof(IAccessTupleProjection), projection);
        }
        private static void AddMapping(IServiceCollection services)
        {
            services.AddAutoMapper(ctg => { } ,typeof(RequestMappingProfile));
        }
    }
}
