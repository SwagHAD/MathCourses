using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdateStudentHandler(IMathDbContext DbContext) : IRequestHandler<UpdateStudentCommand, Student>
    {
        public async Task<Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if(!await DbContext.Set<Student>().AnyAsync(f => f.ID == request.ID))
                throw new ArgumentException(nameof(request));
            await DbContext.Set<Student>().Where(f => f.ID == request.ID)
                .ExecuteUpdateAsync(setters => setters.
                    SetProperty(f => f.Name, request.Name));
            return await DbContext.Set<Student>().AsNoTracking().FirstOrDefaultAsync(f => f.ID == request.ID);
        }
    }
}
