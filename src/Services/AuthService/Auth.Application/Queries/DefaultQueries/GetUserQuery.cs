using Application.Responses.DefaultResponses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetUserQuery : IQuery<UserResponse>
    {
        public int Id { get; init; }
    }
}
