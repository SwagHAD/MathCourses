using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.CourseDto
{
    public class DeleteCourseDto : IDataTransferObjectBase<Course>
    {
        public int Id { get; set; }
    }
}
