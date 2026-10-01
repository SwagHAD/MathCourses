using Application.Queries.Base;
using Application.Responses.DefaultResponses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetUserQuery : IQuery<UserResponse>
    {
        public int Id { get; init; }
    }
}
