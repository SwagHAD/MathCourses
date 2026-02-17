using Application.Commands.CreateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateLessonHandler(IMapper Mapper, IMathDbContext DbContext) : IRequestHandler<CreateLessonCommand, int>
    {
        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = Mapper.Map<Lesson>(request);
            await DbContext.AddAsync(lesson);
            await DbContext.SaveChangesAsync();
            return lesson.ID;
        }
    }
}
