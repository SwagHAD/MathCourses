using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateCourseHandler(IMapper Mapper, IMathDbContext DbContext) : IRequestHandler<CreateCourseCommand, int>
    {
        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Mapper.Map<Course>(request);
            await DbContext.AddAsync(course, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return course.ID;
        }
    }
}
