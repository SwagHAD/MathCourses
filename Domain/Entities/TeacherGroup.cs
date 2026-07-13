using Domain.Attributes;

namespace Domain.Entities
{
    [Title("УчительГруппа")]
    public sealed class TeacherGroup
    {
        public int TeacherID { get; set; }
        public Teacher TeacherRef { get; set; }
        public int GroupID { get; set; }
        public Group GroupRef { get; set; }
    }
}
