using Application.Commands.UpdateCommands;
using Application.Responses;
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
                .FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken) 
                ?? throw new NotFoundException(typeof(Group).GetDescription(), request.ID);
            Mapper.Map(request, group);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
