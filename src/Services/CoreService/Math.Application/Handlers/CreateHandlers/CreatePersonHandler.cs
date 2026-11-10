using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Events;
using MediatR;
using System.Text.Json;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.CreateHandlers
{
    /// <summary>
    /// Оба сохранения — в одной транзакции команды (TransactionBehavior):
    /// человек и запрос на учётку появляются вместе или не появляются вовсе.
    /// </summary>
    public sealed class CreatePersonHandler(IMapper mapper, ISwagDbContext dbContext) : IRequestHandler<CreatePersonCommand, Unit>
    {
        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = mapper.Map<Person>(request.Data);
            await dbContext.Set<Person>().AddAsync(person, cancellationToken);
            // Id нужен событию — сохраняем до записи в outbox
            await dbContext.SaveChangesAsync(cancellationToken);

            if (request.Data.Account is { } account)
            {
                await dbContext.Set<OutboxMessage>().AddAsync(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = nameof(PersonAccountRequestedEvent),
                    Payload = JsonSerializer.Serialize(new PersonAccountRequestedEvent
                    {
                        PersonId = person.Id,
                        Login = account.Login,
                        Password = account.Password,
                        RoleId = account.RoleId
                    })
                }, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
