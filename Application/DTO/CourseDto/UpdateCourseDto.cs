using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.CourseDto
{
    public class UpdateCourseDto : IDataTransferObjectBase<Course>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
