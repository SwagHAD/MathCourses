using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateTeacherHandler(IMapper Mapper, IMathDbContext DbContext) : IRequestHandler<CreateTeacherCommand, Teacher>
    {
        public async Task<Teacher> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = Mapper.Map<Teacher>(request);
            await DbContext.Teachers.AddAsync(teacher, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return teacher;
        }
    }
}
