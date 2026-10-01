using Application.Queries.Base;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetUserPermissionsQuery : IQuery<IReadOnlySet<string>>
    {
    }
}
