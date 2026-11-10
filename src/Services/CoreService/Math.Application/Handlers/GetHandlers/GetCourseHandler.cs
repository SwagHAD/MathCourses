using Application.Queries.DefaultQueries;
using CoreService.Contracts.Courses;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetCourseHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCourseQuery, CourseResponse>
    {
        public async Task<CourseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Course>().AsNoTracking().Where(c => c.Id == request.Id)
            .Select(c => new CourseResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Course).GetDescription(), request.Id);
        }
    }
}
