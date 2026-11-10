using Application.Queries.DefaultQueries;
using CoreService.Contracts.Groups;
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
            return await swagDbContext.Set<Group>().AsNoTracking().Where(g => g.Id == request.Id)
                .Select(g => new GroupDetailsResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    CourseId = g.CourseId,
                    CourseName = g.CourseRef.Name,
                    Members = g.Members
                        .OrderBy(m => m.Kind).ThenBy(m => m.PersonRef.FullName)
                        .Select(m => new GroupMemberResponse
                        {
                            PersonId = m.PersonId,
                            FullName = m.PersonRef.FullName,
                            Kind = m.Kind,
                            Status = m.Status,
                            JoinedAt = m.JoinedAt
                        }).ToArray()
                }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Group).GetDescription(), request.Id);
        }
    }
}
