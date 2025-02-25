using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.StudentMR.Command.CreateStudent
{
    public class CreateStudentCommandHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<CreateStudentCommand, Student>
    {
        public async Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Name = request.Name
            };
            await studentRepository.AddAsync(student, cancellationToken);
            return student;
        }
    }
}
