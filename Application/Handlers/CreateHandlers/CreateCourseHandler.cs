using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateCourseHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateCourseCommand, DefaultCourseResponse>
    {
        public async Task<DefaultCourseResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Mapper.Map<Course>(request);
            await DbContext.AddAsync(course, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<DefaultCourseResponse>(course);
        }
    }
}
