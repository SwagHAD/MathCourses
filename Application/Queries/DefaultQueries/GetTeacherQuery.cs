using Application.Queries.Base;
using Application.Responses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetTeacherQuery : IQuery<DefaultTeacherResponse>
    {
        public int Id { get; init; }
    }
}
