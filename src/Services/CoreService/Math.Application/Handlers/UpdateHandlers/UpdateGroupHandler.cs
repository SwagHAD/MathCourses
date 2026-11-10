using Application.Commands.UpdateCommands;
using CoreService.Contracts.Groups;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.UpdateHandlers
{
    /// <summary>
    /// Состав обновляется построчно, а не заменой коллекции: у оставшихся участников
    /// сохраняется JoinedAt, а будущий перехватчик кортежей видит точные Added/Modified/Deleted.
    /// </summary>
    public sealed class UpdateGroupHandler(ISwagDbContext dbContext) : IRequestHandler<UpdateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var data = request.Data;
            var group = await dbContext.Set<Group>()
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == data.Id, cancellationToken)
                ?? throw new NotFoundException(typeof(Group).GetDescription(), data.Id);

            group.Name = data.Name;
            group.CourseId = data.CourseId;
            SyncMembers(group, data.Members);

            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private static void SyncMembers(Group group, IReadOnlyList<GroupMemberRequest> requested)
        {
            var now = DateTimeOffset.UtcNow;
            var byPerson = requested.ToDictionary(m => m.PersonId);

            group.Members.RemoveAll(m => !byPerson.ContainsKey(m.PersonId));

            foreach (var member in group.Members)
            {
                var wanted = byPerson[member.PersonId];
                member.Kind = wanted.Kind;
                if (member.Status != wanted.Status)
                {
                    member.Status = wanted.Status;
                    member.LeftAt = wanted.Status == MembershipStatus.Active ? null : now;
                }
            }

            var existing = group.Members.Select(m => m.PersonId).ToHashSet();
            group.Members.AddRange(requested
                .Where(m => !existing.Contains(m.PersonId))
                .Select(m => new GroupMembership
                {
                    PersonId = m.PersonId,
                    Kind = m.Kind,
                    Status = m.Status,
                    JoinedAt = now,
                    LeftAt = m.Status == MembershipStatus.Active ? null : now
                }));
        }
    }
}
