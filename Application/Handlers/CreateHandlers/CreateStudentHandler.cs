using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateStudentHandler(IMapper Mapper, IMathDbContext DbContext) : IRequestHandler<CreateStudentDto, Student>
    {
        public async Task<Student> Handle(CreateStudentDto request, CancellationToken cancellationToken)
        {
            var student = Mapper.Map<Student>(request);
            await DbContext.Set<Student>().AddAsync(student, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return student;
        }
    }
}
