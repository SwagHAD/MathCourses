using Application.Queries.Base;
using Application.Responses;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetStudentQuery : IQuery<DefaultStudentResponse>
    {
        public int Id { get; set; }
    }
}
