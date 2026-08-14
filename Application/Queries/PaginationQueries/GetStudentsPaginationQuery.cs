using Application.Queries.Base;
using Application.Responses;

namespace Application.Queries.PaginationQueries
{
    public sealed record GetStudentsPaginationQuery : IQuery<DefaultStudentResponse[]>
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public string NameSearch { get; set; } = null!;
    }
}
