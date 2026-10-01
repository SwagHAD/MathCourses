using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Messaging.Consumers
{
    internal sealed class StudentCreationRequestedConsumer(IAuthDbContext authDbContext, IPasswordHasher<User> passwordHasher, IBus bus) 
        : IConsumer<StudentCreationRequestedEvent>
    {
        public async Task Consume(ConsumeContext<StudentCreationRequestedEvent> context)
        {
            var message = context.Message;
            var user = new User
            {
                Login = message.Login,
                PasswordHash = passwordHasher.HashPassword(null!, message.Password),
                RoleId = message.RoleId
            };
            await authDbContext.Users.AddAsync(user, context.CancellationToken);
            await authDbContext.SaveChangesAsync(context.CancellationToken);

            await bus.Publish(new StudentUserCreatedEvent
            {
                StudentId = message.StudentId,
                UserId = user.Id
            }, context.CancellationToken);
        }
    }
}
