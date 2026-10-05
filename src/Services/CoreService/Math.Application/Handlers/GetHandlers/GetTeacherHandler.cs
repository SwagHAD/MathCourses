using Application.Queries.DefaultQueries;
using Application.Responses;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetTeacherHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetTeacherQuery, DefaultTeacherResponse>
    {
        public async Task<DefaultTeacherResponse> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Teacher>().AsNoTracking().Where(f => f.ID == request.Id).Select(s => new DefaultTeacherResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Teacher).GetDescription(), request.Id);
        }
    }
}
