using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCountOfPersonsQuery : IQuery<int> {}
}
