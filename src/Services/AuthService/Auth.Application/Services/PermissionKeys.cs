using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Access;
using SharedKernel.Application.Interfaces;

namespace Application.Services
{
    /// <summary>Ключи прав для кэша — единственное место в AuthService, где они собираются.</summary>
    internal static class PermissionKeys
    {
        /// <summary>Ключи прав роли в формате <see cref="PermissionKey"/>: "CoreService:Lesson:Read:Related".</summary>
        public static async Task<string[]> LoadForRoleAsync(ISwagDbContext dbContext, int? roleId, CancellationToken cancellationToken)
        {
            if (roleId is null)
                return [];

            var grants = await dbContext.Set<RolePermission>().AsNoTracking()
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => new
                {
                    rp.PermissionRef.ObjectTypeRef.ServiceType,
                    rp.PermissionRef.ObjectType,
                    rp.PermissionRef.ActionType,
                    rp.Scope
                })
                .Distinct()
                .ToArrayAsync(cancellationToken);

            return grants
                .Select(g => PermissionKey.Format(g.ServiceType, g.ObjectType, g.ActionType, g.Scope))
                .ToArray();
        }
    }
}
