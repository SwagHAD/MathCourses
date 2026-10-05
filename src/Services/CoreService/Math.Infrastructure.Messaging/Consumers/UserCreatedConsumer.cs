using Infrastructure.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Messaging.Consumers
{
    public sealed class UserCreatedConsumer(ISwagDbContext dbContext)
        : IConsumer<StudentUserCreatedEvent>, IConsumer<TeacherUserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<StudentUserCreatedEvent> context)
        {
            await dbContext.Set<Student>().Where(s => s.ID == context.Message.StudentId)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.UserId, context.Message.UserId));
        }

        public async Task Consume(ConsumeContext<TeacherUserCreatedEvent> context)
        {
            await dbContext.Set<Teacher>().Where(t => t.ID == context.Message.TeacherId)
                .ExecuteUpdateAsync(t => t.SetProperty(t => t.UserId, context.Message.UserId));
        }
    }
}
