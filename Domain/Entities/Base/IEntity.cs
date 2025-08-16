namespace Domain.Entities.Base
{
    /// <summary>
    /// Базовый интерфейс, которые реализуют все сущности системы
    /// </summary>
    public interface IEntity
    {
        int ID { get; set; }
    }
}
