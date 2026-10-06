using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.Application.Interfaces;
using SharedKernel.Enums;
using SharedKernel.Models;
using SharedKernel.Tools;

namespace Application.Services
{
    /// <summary>
    /// Регистрирует типы объектов микросервиса: добавляет новые, обновляет названия
    /// и создаёт недостающие доступы, сразу выдавая их роли администратора.
    /// Идемпотентен: повторная регистрация тех же типов ничего не меняет.
    /// </summary>
    public sealed class ObjectTypeRegistrar(ISwagDbContext dbContext, ILogger<ObjectTypeRegistrar> logger)
    {
        public async Task RegisterAsync(ServiceType serviceType, IReadOnlyCollection<ObjectTypeDescriptor> descriptors,
            CancellationToken cancellationToken = default)
        {
            var names = descriptors.Select(d => d.Name).ToArray();
            var existing = await dbContext.Set<ObjectType>()
                .Where(x => names.Contains(x.Name))
                .ToDictionaryAsync(x => x.Name, cancellationToken);

            var registered = new List<ObjectType>();
            foreach (var descriptor in descriptors)
            {
                if (!existing.TryGetValue(descriptor.Name, out var objectType))
                {
                    objectType = new ObjectType { Name = descriptor.Name, ServiceType = serviceType, NormalizedName = descriptor.Title };
                    await dbContext.Set<ObjectType>().AddAsync(objectType, cancellationToken);
                }
                else if (objectType.ServiceType != serviceType)
                {
                    logger.LogError("Тип объекта {Name} уже зарегистрирован сервисом {Owner}, регистрация от {ServiceType} пропущена",
                        descriptor.Name, objectType.ServiceType, serviceType);
                    continue;
                }
                else
                {
                    objectType.NormalizedName = descriptor.Title;
                }
                registered.Add(objectType);
            }
            await dbContext.SaveChangesAsync(cancellationToken);

            await RegisterPermissionsAsync(registered, cancellationToken);
            logger.LogInformation("Зарегистрировано типов объектов {ServiceType}: {Count}", serviceType, registered.Count);
        }

        private async Task RegisterPermissionsAsync(IReadOnlyCollection<ObjectType> objectTypes, CancellationToken cancellationToken)
        {
            var names = objectTypes.Select(x => x.Name).ToArray();
            var existing = (await dbContext.Set<Permission>()
                .Where(p => names.Contains(p.ObjectType))
                .Select(p => new { p.ObjectType, p.ActionType })
                .ToArrayAsync(cancellationToken))
                .Select(p => (p.ObjectType, p.ActionType))
                .ToHashSet();

            var adminRoleId = await dbContext.Set<Role>()
                .Where(r => r.UserType == RoleType.Admin)
                .OrderBy(r => r.Id)
                .Select(r => (int?)r.Id)
                .FirstOrDefaultAsync(cancellationToken);

            foreach (var objectType in objectTypes)
            {
                foreach (var action in Enum.GetValues<ActionType>())
                {
                    if (existing.Contains((objectType.Name, action)))
                        continue;

                    var permission = new Permission
                    {
                        ObjectType = objectType.Name,
                        Name = $"{action.GetDescription()} {objectType.NormalizedName}",
                        ActionType = action,
                    };
                    if (adminRoleId is int roleId)
                        permission.RolePermissions.Add(new RolePermission { RoleId = roleId });

                    await dbContext.Set<Permission>().AddAsync(permission, cancellationToken);
                }
            }
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
