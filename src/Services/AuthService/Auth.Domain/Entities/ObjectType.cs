using SharedKernel.Attributes;
using SharedKernel.Entities;
using SharedKernel.Enums;

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
