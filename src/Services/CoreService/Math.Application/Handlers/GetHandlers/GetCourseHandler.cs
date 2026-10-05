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
    public sealed class GetCourseHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCourseQuery, DefaultCourseResponse>
    {
        public async Task<DefaultCourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Course>().AsNoTracking().Where(f => f.ID == request.Id)
            .Select(s => new DefaultCourseResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Course).GetDescription(), request.Id);
        }
    }
}
