using Application;
using Auth.Seeds.Options;
using Infrasctrure;
using Infrasctrure.Options;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Options;

namespace Auth.Api;

public static class ConfigurationAPI
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName) ?? throw new ArgumentNullException("Jwt section is not configured"))
            .ValidateDataAnnotations()
            .Validate(options => !string.IsNullOrEmpty(options.SecretKey), "Jwt section is missing or SecretKey is not set")
            .Validate(options => !string.IsNullOrEmpty(options.Issuer), "Jwt section is missing or Issuer is not set")
            .Validate(options => !string.IsNullOrEmpty(options.Audience), "Jwt section is missing or Audience is not set")
            .ValidateOnStart();

        services.AddOptions<SeedOptions>()
            .Bind(configuration.GetSection(SeedOptions.SectionName) ?? throw new ArgumentNullException("Seed section is not configured"))
            .ValidateDataAnnotations()
            .Validate(options => !string.IsNullOrEmpty(options.AdminLogin), "Seed section is missing or AdminLogin is not set")
            .Validate(options => !string.IsNullOrEmpty(options.AdminPassword), "Seed section is missing or AdminPassword is not set")
            .ValidateOnStart();

        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(options => !string.IsNullOrEmpty(options.Host), "RabbitMq section is missing or Host is not set")
            .Validate(options => !string.IsNullOrEmpty(options.Username), "RabbitMq section is missing or Username is not set")
            .Validate(options => !string.IsNullOrEmpty(options.Password), "RabbitMq section is missing or Password is not set")
            .ValidateOnStart();

        services.AddInfrastructureServices(configuration).AddInfrastructureMessaging(configuration)
            .AddApplication();
    }
}
