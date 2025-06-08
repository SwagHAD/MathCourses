using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Queries
{
    public class GetAllStudentsDetailQueryHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<GetAllStudentsDetailsQuery, ICollection<Student>>
    {
        public async Task<ICollection<Student>> Handle(GetAllStudentsDetailsQuery request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetAllAsync(cancellationToken);
        }
    }
}
