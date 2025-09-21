using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.TeacherDto
{
    public class CreateTeacherDto : IDataTransferObjectBase<Teacher>
    {
        public string Name { get; set; }
    }
}
