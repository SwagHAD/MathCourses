using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<Group>
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
