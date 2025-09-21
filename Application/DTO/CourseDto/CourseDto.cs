using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.CourseDto
{
    public class CourseDto : IDataTransferObjectBase<Course>
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
