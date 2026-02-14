using Application.DTO.Base;
using Application.Mapping.Base;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateGroupDto : IDtoBaseUpdate<Group> , IMapWith<Group>, IRequest<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public List<int> Students { get; set; } = [];
    }
}
