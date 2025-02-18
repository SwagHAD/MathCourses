using DataMath.Entities;
using MediatR;


namespace BLL.MediatoR.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<Group>
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
    }
}
