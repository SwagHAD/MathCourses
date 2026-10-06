using Application.Services;
using Domain.Entities;
using SharedKernel.Enums;
using SharedKernel.Tools;

namespace Auth.Seeds.Seeders
{
    /// <summary>
    /// Регистрирует собственные типы объектов AuthService.
    /// Типы остальных микросервисов приходят событием <c>ObjectTypesRegisteredEvent</c>.
    /// </summary>
    public static class ObjectTypeSeeder
    {
        public static Task SeedAsync(ObjectTypeRegistrar registrar, CancellationToken cancellationToken = default)
        {
            return registrar.RegisterAsync(ServiceType.AuthService, typeof(User).Assembly.GetSecuredObjectTypes(), cancellationToken);
        }
    }
}
