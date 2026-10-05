using Application.Commands.UpdateCommands;
using SharedKernel.Tools;
using AutoMapper;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateCourseHandler(ISwagDbContext swagDbContext, IMapper mapper) : IRequestHandler<UpdateCourseCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await swagDbContext.Set<Course>().FirstOrDefaultAsync(f => f.ID == request.ID)
                ?? throw new NotFoundException(typeof(Course).GetDescription(), request.ID);
            mapper.Map(request, course);
            await swagDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
