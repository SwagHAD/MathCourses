using SharedKernel.Attributes;
using SharedKernel.Models;
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
        /// <summary>Находит в сборке сущности, помеченные <see cref="TitleAttribute.Secured"/>.</summary>
        public static ObjectTypeDescriptor[] GetSecuredObjectTypes(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.GetCustomAttribute<TitleAttribute>() is { Secured: true })
                .Select(type => new ObjectTypeDescriptor { Name = type.Name, Title = type.GetDescription() })
                .OrderBy(descriptor => descriptor.Name)
                .ToArray();
        }
    }
}
