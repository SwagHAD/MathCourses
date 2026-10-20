using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Tools;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateStudentHandler(ISwagDbContext DbContext, IMapper Mapper) 
        : IRequestHandler<UpdateStudentCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await DbContext.Set<Student>().FirstOrDefaultAsync(f => f.StudentId == request.Data.Id, cancellationToken) 
                ?? throw new NotFoundException(typeof(Student).GetDescription(), request.Data.Id);
            Mapper.Map(request.Data, student);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
