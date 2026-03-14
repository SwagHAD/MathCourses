using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateGroupHandler(IMathDbContext DbContext, IMapper Mapper) : IRequestHandler<UpdateGroupCommand, Group>
    {
        public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await DbContext.Set<Group>()
                .Include(g => g.TeacherGroups)
                .Include(g => g.StudentGroups)
                .FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken);

            if (group is null)
                throw new ArgumentException(nameof(request));

            Mapper.Map(request, group);

            await DbContext.SaveChangesAsync(cancellationToken);
            return group;
        }
    }
}
