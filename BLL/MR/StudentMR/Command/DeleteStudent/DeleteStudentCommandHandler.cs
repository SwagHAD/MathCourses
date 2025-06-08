using BLL.Common.Exceptions;
using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Command.DeleteStudent
{
    public class DeleteStudentCommandHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<DeleteStudentCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if(student is null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }
            await studentRepository.DeleteAsync(student, cancellationToken);
            return Unit.Value;
        }
    }
}
