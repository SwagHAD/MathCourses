using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetUserPermissionsQuery : IQuery<IReadOnlySet<string>>
    {
    }
}
