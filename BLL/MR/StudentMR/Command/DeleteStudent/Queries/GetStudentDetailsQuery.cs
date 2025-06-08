using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Queries
{
    public class GetStudentDetailsQuery : IRequest<Student>
    {
        public int Id { get; set; }
    }
}
