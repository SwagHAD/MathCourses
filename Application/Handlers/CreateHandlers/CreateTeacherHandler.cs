using Application.Commands.CreateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateTeacherHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateTeacherCommand, DefaultTeacherResponse>
    {
        public async Task<DefaultTeacherResponse> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = Mapper.Map<Teacher>(request);
            await DbContext.Teachers.AddAsync(teacher, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<DefaultTeacherResponse>(teacher);
        }
    }
}
