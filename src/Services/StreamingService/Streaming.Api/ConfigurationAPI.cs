using StreamingService.Application;
using StreamingService.Application.Common.Interfaces;
using StreamingService.Infrastructure;
using StreamingService.Infrastructure.Options;

namespace StreamingService
{
    public static class ConfigurationAPI
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<MediaMtxOptions>()
                .Bind(configuration.GetSection(MediaMtxOptions.SectionName) ?? throw new ArgumentNullException("MediaMtx section is not configured"))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.ApiUrl), "MediaMtx section is missing or ApiUrl is not set")
                .Validate(options => !string.IsNullOrEmpty(options.PublicUrl), "MediaMtx section is missing or PublicUrl is not set")
                .ValidateOnStart();
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(JwtOptions.SectionName) ?? throw new ArgumentNullException("Jwt section is not configured"))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.SecretKey), "Jwt section is missing or SecretKey is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Issuer), "Jwt section is missing or Issuer is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Audience), "Jwt section is missing or Audience is not set")
                .ValidateOnStart();

            services.AddApplication()
                .AddInfrastructure(configuration);
        }

        public static async Task MigrateAsync(IServiceProvider serviceProvider, CancellationToken ct = default)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IStreamDbContext>();
            await dbContext.MigrateAsync(ct);
        }
    }
}
