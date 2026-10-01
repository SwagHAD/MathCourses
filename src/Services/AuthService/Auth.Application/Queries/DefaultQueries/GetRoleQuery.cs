using Application.Queries.Base;
using Application.Responses.DefaultResponses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetRoleQuery : IQuery<RoleResponse>
    {
        public int Id { get; init; }
    }
}
