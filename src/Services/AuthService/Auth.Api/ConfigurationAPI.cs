using Application;
using Auth.Seeds.Options;
using Infrasctrure;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Options;
using SharedKernel.AspNetCore;
using SharedKernel.Enums;

namespace Auth.Api;

public static class ConfigurationAPI
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedKernel(configuration, ServiceType.AuthService);

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
