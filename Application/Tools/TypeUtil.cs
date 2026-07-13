using Domain.Attributes;
using System.Reflection;

namespace Application.Tools
{
    public static class TypeUtil
    {
        public static string GetDescription(this Type type)
        {
            return type.GetCustomAttribute<TitleAttribute>()?.Description
                ?? throw new InvalidOperationException(
                    $"Тип '{type.FullName}' не содержит атрибут {nameof(TitleAttribute)}.");
        }
        public static string GetDescription(this Enum authenum)
        {
            return authenum.GetType()
                .GetMember(authenum.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<TitleAttribute>()?
                .Description
                ?? throw new InvalidOperationException(
                    $"Элемент перечисления '{authenum.GetType().FullName}.{authenum}' не содержит атрибут {nameof(TitleAttribute)}.");
        }
    }
}
