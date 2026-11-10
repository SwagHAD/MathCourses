using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Материал урока", Secured = true)]
    public sealed class LessonMaterial : BaseEntity
    {
        public long Id { get; set; }
        public long LessonId { get; set; }
        public Lesson LessonRef { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
    }
}
