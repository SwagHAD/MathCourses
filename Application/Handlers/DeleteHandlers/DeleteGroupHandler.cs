using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteGroupHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteGroupCommand, DefaultGroupResponse>
    {
        public async Task<DefaultGroupResponse> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            if (await DbContext.Set<Group>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Group>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return null;
        }
    }
}
