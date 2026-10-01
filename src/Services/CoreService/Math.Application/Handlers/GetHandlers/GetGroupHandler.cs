using Application.Interfaces;
using Application.Queries.DefaultQueries;
using Application.Responses;
using Application.Tools;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetGroupHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetGroupQuery, GroupResponse>
    {
        public async Task<GroupResponse> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Groups.AsNoTracking().Where(f => f.ID == request.Id)
                .Select(s => new GroupResponse
                {
                    Id = s.ID,
                    Name = s.Name,
                    Students = s.StudentGroups.Select(f => new DefaultStudentResponse
                    {
                        Id = f.StudentRef.ID,
                        Name = f.StudentRef.Name
                    }).ToArray(),
                    Teachers = s.TeacherGroups.Select(f => new DefaultTeacherResponse
                    {
                        Id = f.TeacherRef.ID,
                        Name = f.TeacherRef.Name
                    }).ToArray()
                }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Group).GetDescription(), request.Id);
        }
    }
}
