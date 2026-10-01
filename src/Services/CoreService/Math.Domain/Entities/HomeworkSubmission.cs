using Domain.Attributes;
using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    [Title("Сдача домашнего задания")]
    public sealed class HomeworkSubmission : BaseEntity
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public Homework HomeworkRef { get; set; }
        public int StudentId { get; set; }
        public Student StudentRef { get; set; }
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public SubmissionStatus Status { get; set; }
    }
}
