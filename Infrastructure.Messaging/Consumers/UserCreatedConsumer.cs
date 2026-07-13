using Application.Interfaces;
using Infrastructure.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Messaging.Consumers
{
    public sealed class UserCreatedConsumer(ISwagDbContext dbContext)
        : IConsumer<StudentUserCreatedEvent>, IConsumer<TeacherUserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<StudentUserCreatedEvent> context)
        {
            await dbContext.Students.Where(s => s.ID == context.Message.StudentId)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.UserId, context.Message.UserId));
        }

        public async Task Consume(ConsumeContext<TeacherUserCreatedEvent> context)
        {
            await dbContext.Teachers.Where(t => t.ID == context.Message.TeacherId)
                .ExecuteUpdateAsync(t => t.SetProperty(t => t.UserId, context.Message.UserId));
        }
    }
}
