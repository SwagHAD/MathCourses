using CoreService.Contracts.Students;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetStudentQuery : IQuery<StudentResponse>
    {
        public int Id { get; init; }
    }
}
