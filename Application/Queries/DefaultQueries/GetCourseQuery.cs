using Application.Queries.Base;
using Application.Responses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCourseQuery : IQuery<DefaultCourseResponse>
    {
        public int Id { get; set; }
    }
}
