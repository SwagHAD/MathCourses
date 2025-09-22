using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public class UpdateGroupDto : IDataTransferObjectBase<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public List<int> Students { get; set; } = [];
    }
}
