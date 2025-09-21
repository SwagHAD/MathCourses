using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.TeacherDto
{
    public class DeleteTeacherDto : IDataTransferObjectBase<Teacher>
    {
        public int Id { get; set; }
    }
}
