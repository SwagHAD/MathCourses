using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<Group>
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
