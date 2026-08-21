using Application.Queries.Base;
using Application.Responses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetGroupQuery : IQuery<GroupResponse>
    {
        public int Id { get; init; }
    }
}
