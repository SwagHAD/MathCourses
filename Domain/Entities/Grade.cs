using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities
{
    [Title("Оценка")]
    public sealed class Grade : BaseEntity
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public HomeworkSubmission SubmissionRef { get; set; }
        public int TeacherId { get; set; }
        public Teacher TeacherRef { get; set; } = null!;
        public short Score { get; set; }
        public string Comment { get; set; }
    }
}
