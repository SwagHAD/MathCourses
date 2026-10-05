using Application.Responses.DefaultResponses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetPermissionQuery : IQuery<PermissionResponse>
    {
        public int Id { get; init; }
    }
}
