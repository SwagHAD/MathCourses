using SharedKernel.Attributes;
using SharedKernel.Entities;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Сдача задания. Занятие задаёт группу (кто проверяет) и срок сдачи.
    /// </summary>
    [Title("Сдача домашнего задания", Secured = true)]
    public sealed class HomeworkSubmission : BaseEntity
    {
        public long Id { get; set; }
        public long HomeworkId { get; set; }
        public Homework HomeworkRef { get; set; } = null!;
        public long SessionId { get; set; }
        public Session SessionRef { get; set; } = null!;
        public long PersonId { get; set; }
        public Person PersonRef { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public SubmissionStatus Status { get; set; }
    }
}
