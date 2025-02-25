using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.StudentMR.Command.DeleteStudent
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
