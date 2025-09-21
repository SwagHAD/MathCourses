using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.LessonDto
{
    public class CreateLessonDto : IDataTransferObjectBase<Lesson>
    {
        public string Name { get; set; }
    }
}
