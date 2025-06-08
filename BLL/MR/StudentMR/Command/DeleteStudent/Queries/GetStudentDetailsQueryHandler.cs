using BLL.Common.Exceptions;
using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Queries
{
    public class GetStudentDetailsQueryHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<GetStudentDetailsQuery, Student>
    {
        public async Task<Student> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if(student is null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }
            return student;
        }
    }
}
