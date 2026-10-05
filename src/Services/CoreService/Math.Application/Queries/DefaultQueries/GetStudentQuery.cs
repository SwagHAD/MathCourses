using Application.Responses;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetStudentQuery : IQuery<DefaultStudentResponse>
    {
        public int Id { get; init; }
    }
}
