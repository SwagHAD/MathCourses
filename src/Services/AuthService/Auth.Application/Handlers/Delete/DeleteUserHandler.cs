using Application.Commands.Delete;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Delete
{
    public sealed class DeleteUserHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await DbContext.Set<User>()
                .AnyAsync(x => x.Id == request.Id, cancellationToken);

            if (!existUser)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            await DbContext.Set<User>().Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
