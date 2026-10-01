using Domain.Attributes;

namespace Domain.Enums
{
    public enum ActionType
    {
        [Title("Создание")]
        Create,
        [Title("Чтение")]
        Read,
        [Title("Обновление")]
        Update,
        [Title("Удаление")]
        Delete
    }
}
