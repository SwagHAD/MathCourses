using MediatR;

namespace BLL.MR.StudentMR.Command.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
