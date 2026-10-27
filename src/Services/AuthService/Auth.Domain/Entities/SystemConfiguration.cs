using SharedKernel.Entities;

namespace Domain.Entities
{
    public sealed class SystemConfiguration : BaseEntity
    {
        public int? RequeredRoleForView { get; set; }
    }
}
