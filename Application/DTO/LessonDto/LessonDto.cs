using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.LessonDto
{
    public class LessonDto : IDataTransferObjectBase<Lesson>
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
