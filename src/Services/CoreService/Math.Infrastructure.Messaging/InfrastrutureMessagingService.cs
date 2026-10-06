using Infrastructure.Messaging.BackgroundServices;
using Infrastructure.Messaging.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging
{
    public static class InfrastrutureMessagingService
    {
        public static IServiceCollection AddInfrastructureMessaging(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(InfrastrutureMessagingService).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                    cfg.Host(options.Host, "/", h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddHostedService<OutboxPublisher>();
            services.AddHostedService<ObjectTypesPublisher>();
            return services;
        }
    }
}
