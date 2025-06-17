using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public List<Student> Students = [];
    }
}
