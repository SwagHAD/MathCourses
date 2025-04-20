using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath.Entities;
using MediatR;


namespace Core.MR.TeacherMR.Command.DeleteTeacher
{
    public class DeleteTeacherCommandHandler(IGenericRepository<Teacher> teacherRepository) : IRequestHandler<DeleteTeacherCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var group = await teacherRepository.GetByIdAsync(request.Id, cancellationToken);
            if (group == null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }
            await teacherRepository.DeleteAsync(group, cancellationToken);
            return Unit.Value;
        }
    }
}
