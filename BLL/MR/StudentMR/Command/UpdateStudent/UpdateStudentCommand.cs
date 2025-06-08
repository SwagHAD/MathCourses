using MediatR;

namespace Core.MR.StudentMR.Command.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
