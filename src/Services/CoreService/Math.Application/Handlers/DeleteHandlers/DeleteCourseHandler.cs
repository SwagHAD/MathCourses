using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteCourseHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteCourseCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Course>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request), nameof(request));
            await DbContext.Set<Course>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
