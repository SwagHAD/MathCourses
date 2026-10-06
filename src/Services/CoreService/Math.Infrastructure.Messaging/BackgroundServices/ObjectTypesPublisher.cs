using Domain.Entities;
using Infrastructure.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedKernel.AspNetCore.Options;
using SharedKernel.Tools;

namespace Infrastructure.Messaging.BackgroundServices
{
    /// <summary>
    /// При старте сообщает AuthService, на какие сущности сервиса выдаются доступы.
    /// AuthService обрабатывает событие идемпотентно, поэтому публикуем при каждом запуске.
    /// </summary>
    public sealed class ObjectTypesPublisher(IBus bus, IOptions<ServiceOptions> serviceOptions,
        ILogger<ObjectTypesPublisher> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var message = new ObjectTypesRegisteredEvent
            {
                ServiceType = serviceOptions.Value.ServiceType,
                ObjectTypes = typeof(Student).Assembly.GetSecuredObjectTypes()
            };
            try
            {
                await bus.Publish(message, stoppingToken);
                logger.LogInformation("Опубликовано типов объектов: {Count}", message.ObjectTypes.Length);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                logger.LogError(ex, "Не удалось опубликовать типы объектов");
            }
        }
    }
}
