using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateGroupHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateGroupCommand, GroupResponse>
    {
        public async Task<GroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = Mapper.Map<Group>(request);
            await DbContext.Set<Group>().AddAsync(group, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<GroupResponse>(group);
        }
    }
}
