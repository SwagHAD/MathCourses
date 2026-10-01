using Application.Commands.Delete;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Delete
{
    public sealed class DeleteRoleHandler(IAuthDbContext DbContext) : IRequestHandler<DeleteRoleCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var existrole = await DbContext.Roles
                .AnyAsync(x => x.Id == request.Id, cancellationToken);

            if (!existrole)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            await DbContext.Roles.Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
