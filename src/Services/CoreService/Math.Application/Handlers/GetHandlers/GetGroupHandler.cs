using Application.Queries.DefaultQueries;
using CoreService.Contracts.Groups;
using CoreService.Contracts.Students;
using CoreService.Contracts.Teachers;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetGroupHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetGroupQuery, GroupDetailsResponse>
    {
        public async Task<GroupDetailsResponse> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Group>().AsNoTracking().Where(f => f.ID == request.Id)
                .Select(s => new GroupDetailsResponse
                {
                    Id = s.ID,
                    Name = s.Name,
                    Students = s.StudentGroups.Select(f => new StudentResponse
                    {
                        Id = f.StudentRef.ID,
                        Name = f.StudentRef.Name
                    }).ToArray(),
                    Teachers = s.TeacherGroups.Select(f => new TeacherResponse
                    {
                        Id = f.TeacherRef.ID,
                        Name = f.TeacherRef.Name
                    }).ToArray()
                }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Group).GetDescription(), request.Id);
        }
    }
}
