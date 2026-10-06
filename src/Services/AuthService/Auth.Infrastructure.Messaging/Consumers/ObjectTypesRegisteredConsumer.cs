using Application.Services;
using Infrastructure.Events;
using MassTransit;

namespace Infrastructure.Messaging.Consumers
{
    internal sealed class ObjectTypesRegisteredConsumer(ObjectTypeRegistrar registrar)
        : IConsumer<ObjectTypesRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<ObjectTypesRegisteredEvent> context)
        {
            await registrar.RegisterAsync(context.Message.ServiceType, context.Message.ObjectTypes, context.CancellationToken);
        }
    }
}
