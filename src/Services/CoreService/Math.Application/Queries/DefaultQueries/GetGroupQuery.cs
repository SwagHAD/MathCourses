using CoreService.Contracts.Groups;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetGroupQuery : IQuery<GroupDetailsResponse>
    {
        public int Id { get; init; }
    }
}
