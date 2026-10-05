using SharedKernel.Attributes;

namespace Domain.Enums
{
    public enum StreamStatus
    {
        [Title("Запланирован")]
        Scheduled,
        [Title("В эфире")]
        Live,
        [Title("Завершён")]
        Ended
    }
}
