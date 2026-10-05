namespace SharedKernel.Entities
{
    /// <summary>Общий контракт доменных сущностей всех микросервисов.</summary>
    public interface IBaseEntity
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
