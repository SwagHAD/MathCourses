using SharedKernel.Attributes;
using System.Reflection;

namespace SharedKernel.Tools
{
    public static class TypeUtil
    {
        public static string GetDescription(this Type type)
        {
            return type.GetCustomAttribute<TitleAttribute>()?.Description
                ?? throw new InvalidOperationException(
                    $"Тип '{type.FullName}' не содержит атрибут {nameof(TitleAttribute)}.");
        }
        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<TitleAttribute>()?
                .Description
                ?? throw new InvalidOperationException(
                    $"Элемент перечисления '{value.GetType().FullName}.{value}' не содержит атрибут {nameof(TitleAttribute)}.");
        }
    }
}
