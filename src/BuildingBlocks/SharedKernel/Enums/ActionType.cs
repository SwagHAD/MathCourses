using SharedKernel.Attributes;

namespace SharedKernel.Enums
{
    public enum ActionType
    {
        [Title("Создание")]
        Create = 0,
        [Title("Чтение")]
        Read = 1,
        [Title("Обновление")]
        Update = 2,
        [Title("Удаление")]
        Delete = 3
    }
}
