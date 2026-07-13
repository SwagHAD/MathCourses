using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Messaging.BackgroundServices
{
    public sealed class OutboxPublisher(IServiceScopeFactory scopeFactory, ILogger<OutboxPublisher> logger) 
        : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(5);
        private const int MaxRetryCount = 3;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessOutboxMessagesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Outbox processing failed");
                }
                await Task.Delay(Interval, stoppingToken);
            }
        }
        
        private async Task ProcessOutboxMessagesAsync(CancellationToken ct)
        {
            await using var scope = scopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ISwagDbContext>();
            var bus = scope.ServiceProvider.GetRequiredService<IBus>();

            var messages = await dbContext.Set<OutboxMessage>()
                .Where(m => m.ProcessedAt == null && m.RetryCount < MaxRetryCount)
                .OrderBy(m => m.CreatedAt)
                .Take(20)
                .ToArrayAsync(ct);

            foreach (var message in messages)
            {
                try
                {
                    var eventType = ResolveType(message.Type);
                    if (eventType is null)
                    {
                        logger.LogWarning("Unknown event type: {Type}", message.Type);
                        continue;
                    }

                    var payload = JsonSerializer.Deserialize(message.Payload, eventType);
                    if (payload is null)
                        continue;

                    await bus.Publish(payload, eventType, ct);

                    message.ProcessedAt = DateTimeOffset.UtcNow;
                    message.Error = null;
                }
                catch (Exception ex)
                {
                    message.RetryCount++;
                    message.Error = ex.Message;
                    logger.LogError(ex, "Failed to publish outbox message {Id}", message.Id);
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
        private static Type? ResolveType(string typeName) => typeName switch
        {
            nameof(StudentCreationRequestedEvent) => typeof(StudentCreationRequestedEvent),
            nameof(TeacherCreationRequestedEvent) => typeof(TeacherCreationRequestedEvent),
            _ => null
        };
    }
}
