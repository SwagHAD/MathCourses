using AutoMapper;
using BLL.Common.Exceptions;
using BLL.MR.StudentMR.Queries.Dto;
using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.StudentMR.Queries
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
