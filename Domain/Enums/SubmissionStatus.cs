using Domain.Attributes;

namespace Domain.Enums
{
    public enum SubmissionStatus
    {
        [Title("Отправлено, ждет проверки")]
        Submitted,
        [Title("Проверено")]
        Reviewed,
        [Title("Сдано после дедлайна")]
        Late
    }
}
