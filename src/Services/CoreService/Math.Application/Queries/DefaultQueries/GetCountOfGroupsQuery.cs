using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCountOfGroupsQuery : IQuery<int> {}
}
