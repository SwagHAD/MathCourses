using Application.Queries.Base;
using Application.Responses.DefaultResponses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetPermissionQuery : IQuery<PermissionResponse>
    {
        public int Id { get; init; }
    }
}
