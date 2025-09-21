using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.TeacherDto
{
    public class UpdateTeacherDto : IDataTransferObjectBase<Teacher>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
