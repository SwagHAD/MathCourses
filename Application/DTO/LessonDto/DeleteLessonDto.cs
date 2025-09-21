using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.LessonDto
{
    public class DeleteLessonDto : IDataTransferObjectBase<Lesson>
    {
        public int Id { get; set; }
    }
}
