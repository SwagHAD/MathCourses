using Domain.Attributes;

namespace Domain.Enums
{
    public enum ActionType
    {
        [Title("Создание")]
        Create = 0,
        [Title("Обновление")]
        Update = 1,
        [Title("Удаление")]
        Delete = 2,
        [Title("Чтение")]
        Read = 3
    }
}
