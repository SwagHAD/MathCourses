using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteLessonHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteLessonCommand, DefaultLessonResponse>
    {
        public async Task<DefaultLessonResponse> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Lesson>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Lesson>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return null;
        }
    }
}
