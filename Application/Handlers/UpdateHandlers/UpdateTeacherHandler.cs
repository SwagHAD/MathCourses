using Application.Commands.UpdateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateTeacherHandler(ISwagDbContext DbContext, IMapper Mapper) : IRequestHandler<UpdateTeacherCommand, DefaultTeacherResponse>
    {
        public async Task<DefaultTeacherResponse> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await DbContext.Set<Teacher>()
                .FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken);

            if (teacher is null)
                throw new ArgumentException(nameof(request));

            Mapper.Map(request, teacher);

            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<DefaultTeacherResponse>(teacher);
        }
    }
}
