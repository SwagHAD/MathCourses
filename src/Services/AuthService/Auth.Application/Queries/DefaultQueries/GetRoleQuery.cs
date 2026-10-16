using AuthService.Contracts.Roles;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetRoleQuery : IQuery<RoleResponse>
    {
        public int Id { get; init; }
    }
}
