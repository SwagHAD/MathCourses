using Domain.Attributes;
using Domain.Entities.Base;

namespace Domain.Entities
{
    /// <summary>
    /// Направление
    /// </summary>
    [Title("Направление")]
    public sealed class Course : BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
