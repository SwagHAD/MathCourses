using Application.Commands.CreateCommands;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateGroupHandler(IMapper Mapper, ISwagDbContext DbContext) : IRequestHandler<CreateGroupCommand, GroupResponse>
    {
        public async Task<GroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = Mapper.Map<Group>(request);
            await DbContext.Set<Group>().AddAsync(group, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return await DbContext.Set<Group>().AsNoTracking().Select(f => new GroupResponse()
            {
                Id = f.ID,
                Name = f.Name,
                Students = f.StudentGroups.Select(f => new DefaultStudentResponse
                {
                    Id = f.StudentID,
                    Name = f.StudentRef.Name,
                }).ToArray(),
                Teachers = f.TeacherGroups.Select(f => new DefaultTeacherResponse
                {
                    Id = f.TeacherID,
                    Name = f.TeacherRef.Name,
                }).ToArray(),
            }).Where(f => f.Id == group.ID).FirstOrDefaultAsync() ?? throw new ArgumentException(nameof(group));
        }
    }
}
