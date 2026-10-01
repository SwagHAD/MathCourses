using Application.Commands.CreateCommands;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Events;
using MediatR;
using System.Text.Json;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateTeacherHandler(IMapper Mapper, ISwagDbContext DbContext) 
        : IRequestHandler<CreateTeacherCommand, Unit>
    {
        public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = Mapper.Map<Teacher>(request);
            await DbContext.Teachers.AddAsync(teacher, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = nameof(TeacherCreationRequestedEvent),
                Payload = JsonSerializer.Serialize(new TeacherCreationRequestedEvent
                {
                    TeacherId = teacher.ID,
                    Login = request.Login,
                    Password = request.Password,
                    RoleId = request.Role
                })
            };
            await DbContext.Set<OutboxMessage>().AddAsync(outboxMessage, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
