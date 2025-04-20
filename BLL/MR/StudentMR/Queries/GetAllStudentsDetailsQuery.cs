using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Queries
{
    public class GetAllStudentsDetailsQuery : IRequest<ICollection<Student>>
    {
    }
}
