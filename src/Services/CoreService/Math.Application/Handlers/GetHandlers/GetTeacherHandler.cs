using Application.Queries.DefaultQueries;
using CoreService.Contracts.Teachers;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetTeacherHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetTeacherQuery, TeacherResponse>
    {
        public async Task<TeacherResponse> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Teacher>().AsNoTracking().Where(f => f.TeacherId == request.Id).Select(s => new TeacherResponse
            {
                Id = s.TeacherId,
                Name = s.PersonRef.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Teacher).GetDescription(), request.Id);
        }
    }
}
