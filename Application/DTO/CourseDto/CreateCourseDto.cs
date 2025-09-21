using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.CourseDto
{
    public class CreateCourseDto : IDataTransferObjectBase<Course>
    {
        public string Name { get; set; }
    }
}
