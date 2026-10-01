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
    public sealed class GetCourseHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCourseQuery, DefaultCourseResponse>
    {
        public async Task<DefaultCourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Courses.AsNoTracking().Where(f => f.ID == request.Id)
            .Select(s => new DefaultCourseResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Course).GetDescription(), request.Id);
        }
    }
}
