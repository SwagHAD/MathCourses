using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.StudentMR.Command.UpdateStudent
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
