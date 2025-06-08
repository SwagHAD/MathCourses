using MediatR;

namespace Core.MR.StudentMR.Command.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
