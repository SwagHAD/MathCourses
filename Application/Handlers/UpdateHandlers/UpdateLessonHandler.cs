using Application.Commands.UpdateCommands;
using Application.Interfaces;
using Application.Tools;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateLessonHandler(ISwagDbContext swagDbContext, IMapper mapper) 
        : IRequestHandler<UpdateLessonCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await swagDbContext.Set<Lesson>().FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken)
                ?? throw new NotFoundException(typeof(Lesson).GetDescription(), request.ID);
            mapper.Map(request, lesson);
            await swagDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
