using Application.Commands.Delete;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Delete
{
    public sealed class DeleteUserHandler(IAuthDbContext DbContext) : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await DbContext.Users
                .AnyAsync(x => x.Id == request.Id, cancellationToken);

            if (!existUser)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            await DbContext.Users.Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
