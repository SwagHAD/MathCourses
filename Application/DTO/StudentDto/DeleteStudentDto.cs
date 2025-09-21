using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.StudentDto
{
    public class DeleteStudentDto : IDataTransferObjectBase<Student>
    {
        public int ID { get; set; }
    }
}
