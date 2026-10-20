using Application.Commands.DeleteCommands;
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
            if(!await DbContext.Set<Student>().AnyAsync(f => f.StudentId == request.Id))
                throw new NotFoundException(nameof(Student), request.Id);
            await DbContext.Set<Person>().Where(f => f.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
