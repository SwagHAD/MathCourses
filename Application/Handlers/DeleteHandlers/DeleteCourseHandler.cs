using Application.Commands.DeleteCommands;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteCourseHandler(IMathDbContext DbContext) : IRequestHandler<DeleteCourseCommand, Course>
    {
        public async Task<Course> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Course>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request), nameof(request));
            await DbContext.Set<Course>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return new Course();
        }
    }
}
