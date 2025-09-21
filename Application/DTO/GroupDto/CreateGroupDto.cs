using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDto
{
    public class CreateGroupDto : IDataTransferObjectBase<Group>
    {
        public string Name { get; set; }

        public int? TeacherID {  get; set; }

        public int? CourseID { get; set; }
    }
}
