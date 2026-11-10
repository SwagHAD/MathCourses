using Infrastructure.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Messaging.Consumers
{
    public sealed class UserCreatedConsumer(ISwagDbContext dbContext) : IConsumer<PersonUserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<PersonUserCreatedEvent> context)
        {
            await dbContext.Set<Person>().Where(p => p.Id == context.Message.PersonId)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.UserId, context.Message.UserId), context.CancellationToken);
        }
    }
}
