using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteLessonHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteLessonCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Lesson>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Lesson>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
