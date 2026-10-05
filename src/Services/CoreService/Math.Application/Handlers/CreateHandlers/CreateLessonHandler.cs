using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateLessonHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateLessonCommand, Unit>
    {
        public async Task<Unit> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = Mapper.Map<Lesson>(request);
            await DbContext.AddAsync(lesson);
            await DbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
