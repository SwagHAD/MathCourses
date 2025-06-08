using BLL.Common.Exceptions;
using DataMath.Entities;
using Core.Repository;
using MediatR;

namespace Core.MR.StudentMR.Command.UpdateStudent
{
    public class UpdateStudentCommandHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<UpdateStudentCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if(student is null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }
            student.Name = request.Name;
            await studentRepository.UpdateAsync(student, cancellationToken);
            return Unit.Value;
        }
    }
}
