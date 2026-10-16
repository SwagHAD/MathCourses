using CoreService.Contracts.Teachers;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetTeacherQuery : IQuery<TeacherResponse>
    {
        public int Id { get; init; }
    }
}
