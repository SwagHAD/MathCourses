using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetCountOfTeachersQuery : IQuery<int> {}
}
