using SharedKernel.Entities;

namespace Domain.Entities
{
    public sealed class Form : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
