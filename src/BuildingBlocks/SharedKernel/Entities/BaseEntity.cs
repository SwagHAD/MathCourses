namespace SharedKernel.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
