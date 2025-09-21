using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.TeacherDto
{
    public class TeacherDto : IDataTransferObjectBase<Teacher>
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
