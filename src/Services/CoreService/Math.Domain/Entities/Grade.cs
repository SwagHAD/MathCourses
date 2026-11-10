using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Оценка", Secured = true)]
    public sealed class Grade : BaseEntity
    {
        public long Id { get; set; }
        public long SubmissionId { get; set; }
        public HomeworkSubmission SubmissionRef { get; set; } = null!;
        /// <summary>Кто поставил оценку.</summary>
        public long GradedById { get; set; }
        public Person GradedByRef { get; set; } = null!;
        public short Score { get; set; }
        public string? Comment { get; set; }
    }
}
