using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteTeacherHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteTeacherCommand, Teacher>
    {
        public async Task<Teacher> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Teacher>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Teacher>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return null;
        }
    }
}
 