using DataMath.Entities;
using MediatR;

namespace Core.MR.TeacherMR.Command.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
