using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateStudentHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateStudentCommand, DefaultStudentResponse>
    {
        public async Task<DefaultStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = Mapper.Map<Student>(request);
            await DbContext.Set<Student>().AddAsync(student, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<DefaultStudentResponse>(student);
        }
    }
}
