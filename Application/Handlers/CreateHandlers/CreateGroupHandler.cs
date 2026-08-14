using Application.Commands.CreateCommands;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateGroupHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = Mapper.Map<Group>(request);
            await DbContext.Set<Group>().AddAsync(group, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
