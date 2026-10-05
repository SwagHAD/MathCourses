using Application.Responses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCourseQuery : IQuery<DefaultCourseResponse>
    {
        public int Id { get; init; }
    }
}
