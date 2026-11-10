using Domain.Entities;
using Infrastructure.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Application.Interfaces;

namespace Infrastructure.Messaging.Consumers
{
    /// <summary>
    /// CoreService создал человека и просит для него учётную запись.
    /// Студент он или преподаватель — Auth не важно: это решает роль (RoleId) и состав групп в Core.
    /// </summary>
    internal sealed class PersonAccountRequestedConsumer(ISwagDbContext authDbContext, IPasswordHasher<User> passwordHasher, IBus bus)
        : IConsumer<PersonAccountRequestedEvent>
    {
        public async Task Consume(ConsumeContext<PersonAccountRequestedEvent> context)
        {
            var message = context.Message;
            var user = new User
            {
                Login = message.Login,
                RoleId = message.RoleId
            };
            user.PasswordHash = passwordHasher.HashPassword(user, message.Password);
            await authDbContext.Set<User>().AddAsync(user, context.CancellationToken);
            await authDbContext.SaveChangesAsync(context.CancellationToken);

            await bus.Publish(new PersonUserCreatedEvent
            {
                PersonId = message.PersonId,
                UserId = user.Id
            }, context.CancellationToken);
        }
    }
}
