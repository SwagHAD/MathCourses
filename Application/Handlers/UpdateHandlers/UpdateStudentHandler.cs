using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateStudentHandler(ISwagDbContext DbContext, IMapper Mapper) : IRequestHandler<UpdateStudentCommand, Student>
    {
        public async Task<Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Student>().AnyAsync(f => f.ID == request.ID, cancellationToken))
                throw new ArgumentException(nameof(request));
            
            var student = await DbContext.Set<Student>().Include(f => f.StudentGroups).FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken);
            Mapper.Map(request, student);
            await DbContext.SaveChangesAsync(cancellationToken);
            return student;
        }
    }
}
