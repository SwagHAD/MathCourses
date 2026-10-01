using Domain.Attributes;
using Domain.Base;
using Domain.Enums;

namespace Domain.Entities
{
    [Title("Тип объекта")]
    public sealed class ObjectType : BaseEntity
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
