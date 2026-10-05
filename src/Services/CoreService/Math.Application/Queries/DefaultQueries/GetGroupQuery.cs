using Application.Responses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetGroupQuery : IQuery<GroupResponse>
    {
        public int Id { get; init; }
    }
}
