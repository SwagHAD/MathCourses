using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public class UpdateGroupDto : IDTOBaseUpdate<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public List<int> Students { get; set; } = [];
    }
}
