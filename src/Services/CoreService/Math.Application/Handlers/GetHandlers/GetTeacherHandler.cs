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
    public sealed class GetTeacherHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetTeacherQuery, DefaultTeacherResponse>
    {
        public async Task<DefaultTeacherResponse> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Teachers.AsNoTracking().Where(f => f.ID == request.Id).Select(s => new DefaultTeacherResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Teacher).GetDescription(), request.Id);
        }
    }
}
