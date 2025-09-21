using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.LessonDto
{
    public class UpdateLessonDto : IDataTransferObjectBase<Lesson>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
