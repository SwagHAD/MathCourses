using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.TeacherMR.Command.CreateTeacher
{
    public class CreateTeacherCommandHandler(IGenericRepository<Teacher> teacherRepository) : IRequestHandler<CreateTeacherCommand, Teacher>
    {
        public async Task<Teacher> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher()
            {
                Name = request.Name,
            };
            await teacherRepository.AddAsync(teacher);
            return teacher;
        }
    }
}
