using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Events;
using MediatR;
using System.Text.Json;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateTeacherHandler(IMapper Mapper, ISwagDbContext DbContext) 
        : IRequestHandler<CreateTeacherCommand, Unit>
    {
        public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = Mapper.Map<Teacher>(request.Data);
            await DbContext.Set<Teacher>().AddAsync(teacher, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = nameof(TeacherCreationRequestedEvent),
                Payload = JsonSerializer.Serialize(new TeacherCreationRequestedEvent
                {
                    TeacherId = teacher.ID,
                    Login = request.Data.Login,
                    Password = request.Data.Password,
                    RoleId = request.Data.RoleId
                })
            };
            await DbContext.Set<OutboxMessage>().AddAsync(outboxMessage, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
