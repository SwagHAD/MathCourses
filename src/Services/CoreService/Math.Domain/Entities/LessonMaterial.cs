using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Материал урока", Secured = true)]
    public sealed class LessonMaterial : BaseEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson LessonRef { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
    }
}
