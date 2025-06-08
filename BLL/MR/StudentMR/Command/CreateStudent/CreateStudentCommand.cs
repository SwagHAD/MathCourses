using DataMath.Entities;
using MediatR;

namespace Core.MR.StudentMR.Command.CreateStudent
{
    public class CreateStudentCommand : IRequest<Student>
    {
        public string Name { get; set; }
    }
}
