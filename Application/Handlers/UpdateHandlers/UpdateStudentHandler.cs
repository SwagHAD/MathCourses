using Application.Commands.UpdateCommands;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Tools;
using Application.Interfaces;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateStudentHandler(ISwagDbContext DbContext, IMapper Mapper) : IRequestHandler<UpdateStudentCommand, DefaultStudentResponse>
    {
        public async Task<DefaultStudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await DbContext.Set<Student>().FirstOrDefaultAsync(f => f.ID == request.ID, cancellationToken) 
                ?? throw new NotFoundException(typeof(Student).GetDescription(), request.ID);
            Mapper.Map(request, student);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<DefaultStudentResponse>(student);
        }
    }
}
