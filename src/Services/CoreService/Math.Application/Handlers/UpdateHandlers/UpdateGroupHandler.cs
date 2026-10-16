using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Tools;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateGroupHandler(ISwagDbContext DbContext, IMapper Mapper) 
        : IRequestHandler<UpdateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await DbContext.Set<Group>()
                .Include(g => g.TeacherGroups)
                .Include(g => g.StudentGroups)
                .FirstOrDefaultAsync(f => f.ID == request.Data.Id, cancellationToken) 
                ?? throw new NotFoundException(typeof(Group).GetDescription(), request.Data.Id);
            Mapper.Map(request.Data, group);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
