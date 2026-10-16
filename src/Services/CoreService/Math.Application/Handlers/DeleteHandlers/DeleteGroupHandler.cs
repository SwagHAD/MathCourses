using Application.Commands.DeleteCommands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteGroupHandler(ISwagDbContext DbContext) 
        : IRequestHandler<DeleteGroupCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            if (await DbContext.Set<Group>().AnyAsync(f => f.ID == request.Id))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Group>().Where(f => f.ID == request.Id).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
