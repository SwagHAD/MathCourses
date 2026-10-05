using Application.Commands.DeleteCommands;
using Application.Responses;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteStudentHandler(ISwagDbContext DbContext) 
        : IRequestHandler<DeleteStudentCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Student>().AnyAsync(f => f.ID == request.ID))
                throw new NotFoundException(nameof(Student), request.ID);
            await DbContext.Set<Student>().Where(f => f.ID == request.ID).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
