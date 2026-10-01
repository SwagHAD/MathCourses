using Application.Commands.CreateCommands;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateCourseHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateCourseCommand, Unit>
    {
        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Mapper.Map<Course>(request);
            await DbContext.AddAsync(course, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
