using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Events;
using MediatR;
using System.Text.Json;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateStudentHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateStudentCommand, Unit>
    {
        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = Mapper.Map<Student>(request);
            await DbContext.Set<Student>().AddAsync(student, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = nameof(StudentCreationRequestedEvent),
                Payload = JsonSerializer.Serialize(new StudentCreationRequestedEvent
                {
                    StudentId = student.ID,
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
