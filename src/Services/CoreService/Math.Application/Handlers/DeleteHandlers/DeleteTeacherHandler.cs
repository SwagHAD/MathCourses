using Application.Commands.DeleteCommands;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteTeacherHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteTeacherCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Teacher>().AnyAsync(f => f.ID == request.Id))
                throw new NotFoundException(nameof(Teacher), request.Id);
            await DbContext.Set<Teacher>().Where(f => f.ID == request.Id).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
 