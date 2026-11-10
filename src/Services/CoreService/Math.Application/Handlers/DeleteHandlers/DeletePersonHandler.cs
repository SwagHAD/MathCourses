using Application.Commands.DeleteCommands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.DeleteHandlers
{
    /// <summary>
    /// Человека, который состоит в группах или что-то сдавал, удалить нельзя (FK Restrict) —
    /// сначала его убирают из групп. Историю участия лучше хранить статусом, а не удалением.
    /// </summary>
    public sealed class DeletePersonHandler(ISwagDbContext dbContext) : IRequestHandler<DeletePersonCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var deleted = await dbContext.Set<Person>().Where(p => p.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            if (deleted == 0)
                throw new NotFoundException(typeof(Person).GetDescription(), request.Id);
            return Unit.Value;
        }
    }
}
