using Application.Commands.CreateCommands;
using Domain.Entities;
using MediatR;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateGroupHandler(ISwagDbContext dbContext) : IRequestHandler<CreateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;
            var group = new Group
            {
                Name = request.Data.Name,
                CourseId = request.Data.CourseId,
                Members = request.Data.Members.Select(m => new GroupMembership
                {
                    PersonId = m.PersonId,
                    Kind = m.Kind,
                    Status = m.Status,
                    JoinedAt = now
                }).ToList()
            };
            await dbContext.Set<Group>().AddAsync(group, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
