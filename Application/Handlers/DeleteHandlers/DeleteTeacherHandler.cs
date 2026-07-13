using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteTeacherHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteTeacherCommand, DefaultTeacherResponse>
    {
        public async Task<DefaultTeacherResponse> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Teacher>().AnyAsync(f => f.ID == request.ID))
                throw new NotFoundException(nameof(Teacher), request.ID);
            await DbContext.Set<Teacher>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return null;
        }
    }
}
 