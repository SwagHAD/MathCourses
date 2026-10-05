using Application;
using Infrastructure;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Options;
using SharedKernel.AspNetCore;
using SharedKernel.Enums;
using SharedKernel.Application.Interfaces;

namespace Math.Api
{
    public static class GeneralConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSharedKernel(configuration, ServiceType.CoreService);

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
