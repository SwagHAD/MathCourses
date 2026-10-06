using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Группы
    /// </summary>
    [Title("Группа", Secured = true)]
    public sealed class Group : BaseEntity
    {
        public int ID { get; set; }
        /// <summary>
        /// Имя группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Учитель
        /// </summary>
        public List<TeacherGroup> TeacherGroups { get; set; } = new();
        /// <summary>
        /// Направление группы
        /// </summary>
        public int? CourseID { get; set; }
        public Course Course { get; set; }
        public List<StudentGroup> StudentGroups { get; set; } = new();
    }
}
