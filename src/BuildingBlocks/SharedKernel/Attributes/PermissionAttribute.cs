using SharedKernel.Enums;

namespace SharedKernel.Attributes
{
    /// <summary>
    /// Доступ к эндпоинту: у пользователя должно быть право на действие с типом объекта в любой области.
    /// Какие именно записи ему видны (Own/Related/All), решает уже фильтр данных, а не этот атрибут.
    /// </summary>
    public sealed class PermissionAttribute : Attribute
    {
        public string ObjectTypeName { get; set; } = null!;
        public ActionType ActionType { get; set; }
    }
}
