using Application.Responses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetTeacherQuery : IQuery<DefaultTeacherResponse>
    {
        public int Id { get; init; }
    }
}
