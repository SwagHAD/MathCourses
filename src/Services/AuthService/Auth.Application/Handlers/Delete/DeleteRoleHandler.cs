using Application.Commands.Delete;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Delete
{
    public sealed class DeleteRoleHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteRoleCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var existrole = await DbContext.Set<Role>()
                .AnyAsync(x => x.Id == request.Id, cancellationToken);

            if (!existrole)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            await DbContext.Set<Role>().Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
