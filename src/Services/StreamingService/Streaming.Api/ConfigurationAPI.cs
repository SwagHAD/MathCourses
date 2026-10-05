using StreamingService.Application;
using StreamingService.Infrastructure;
using StreamingService.Infrastructure.Options;
using SharedKernel.AspNetCore;
using SharedKernel.Enums;
using SharedKernel.Application.Interfaces;

namespace StreamingService
{
    public static class ConfigurationAPI
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSharedKernel(configuration, ServiceType.StreamService);

            services.AddOptions<MediaMtxOptions>()
                .Bind(configuration.GetSection(MediaMtxOptions.SectionName) ?? throw new ArgumentNullException("MediaMtx section is not configured"))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.ApiUrl), "MediaMtx section is missing or ApiUrl is not set")
                .Validate(options => !string.IsNullOrEmpty(options.PublicUrl), "MediaMtx section is missing or PublicUrl is not set")
                .ValidateOnStart();

            services.AddApplication()
                .AddInfrastructure(configuration);
        }

        public static async Task MigrateAsync(IServiceProvider serviceProvider, CancellationToken ct = default)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ISwagDbContext>();
            await dbContext.MigrateAsync(ct);
        }
    }
}
