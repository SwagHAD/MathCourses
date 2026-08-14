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
    public sealed class UpdateTeacherHandler(ISwagDbContext DbContext, IMapper Mapper) 
        : IRequestHandler<UpdateTeacherCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await DbContext.Set<Teacher>().FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken) 
                ?? throw new NotFoundException(typeof(Teacher).GetDescription(), request.ID);
            Mapper.Map(request, teacher);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
