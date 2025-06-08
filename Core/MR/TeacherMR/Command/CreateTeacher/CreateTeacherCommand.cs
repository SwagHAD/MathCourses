using DataMath.Entities;
using MediatR;

namespace Core.MR.TeacherMR.Command.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<Teacher>
    {
        public string Name { get; set; }
    }
}
