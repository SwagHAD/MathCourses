using Application;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Options;
using Infrastructure.Options;

namespace Math.Api
{
    public static class GeneralConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(JwtOptions.SectionName) ?? throw new ArgumentNullException("Jwt section is not configured"))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.SecretKey), "Jwt section is missing or SecretKey is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Issuer), "Jwt section is missing or Issuer is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Audience), "Jwt section is missing or Audience is not set")
                .ValidateOnStart();

            services.AddOptions<RabbitMqOptions>()
                .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
                .ValidateDataAnnotations()
                .Validate(options => !string.IsNullOrEmpty(options.Host), "RabbitMq section is missing or Host is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Username), "RabbitMq section is missing or Username is not set")
                .Validate(options => !string.IsNullOrEmpty(options.Password), "RabbitMq section is missing or Password is not set")
                .ValidateOnStart();

            services.AddApplication()
                .AddInfrastructure(configuration)
                .AddInfrastructureMessaging(configuration);
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider services)
        {
            await using var scope = services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<ISwagDbContext>();
            await dbcontext.MigrateAsync();
        }
    }
}
