using CoreService.Contracts.Courses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCourseQuery : IQuery<CourseResponse>
    {
        public long Id { get; init; }
    }
}
