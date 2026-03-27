using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteStudentHandler(ISwagDbContext DbContext) : IRequestHandler<DeleteStudentCommand, Student>
    {
        public async Task<Student> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Student>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Student>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return null;
        }
    }
}
