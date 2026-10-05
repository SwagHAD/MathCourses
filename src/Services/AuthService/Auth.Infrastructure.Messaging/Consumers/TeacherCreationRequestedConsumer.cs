using Domain.Entities;
using Infrastructure.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Application.Interfaces;

namespace Infrastructure.Messaging.Consumers
{
    internal sealed class TeacherCreationRequestedConsumer(ISwagDbContext authDbContext, IPasswordHasher<User> passwordHasher, IBus bus) 
        : IConsumer<TeacherCreationRequestedEvent>
    {
        public async Task Consume(ConsumeContext<TeacherCreationRequestedEvent> context)
        {
            var message = context.Message;
            var user = new User
            {
                Login = message.Login,
                PasswordHash = passwordHasher.HashPassword(null!, message.Password),
                RoleId = message.RoleId
            };
            await authDbContext.Set<User>().AddAsync(user, context.CancellationToken);
            await authDbContext.SaveChangesAsync(context.CancellationToken);

            await bus.Publish(new TeacherUserCreatedEvent
            {
                TeacherId = message.TeacherId,
                UserId = user.Id
            }, context.CancellationToken);
        }
    }
}
