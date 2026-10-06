using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Уроки
    /// </summary>
    [Title("Урок", Secured = true)]
    public sealed class Lesson : BaseEntity
    {
        public int ID { get; set; }
        /// <summary>
        /// Название урока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public int? GroupID { get; set; }
        public Group GroupRef { get; set; }
    }
}
