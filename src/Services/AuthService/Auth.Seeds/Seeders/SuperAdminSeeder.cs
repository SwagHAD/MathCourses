using SharedKernel.Tools;
using Auth.Seeds.Options;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;
using SharedKernel.Application.Interfaces;

namespace Auth.Api.Seeders;

public static class SuperAdminSeeder
{
    private static int AdminUserID = 1;
    private static int AdminRoleID = 1;
    public static async Task SeedAsync(
        ISwagDbContext dbContext,
        SeedOptions seedOptions,
        CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(dbContext, cancellationToken);
        await SeedUsersAsync(dbContext, seedOptions, cancellationToken);
        await SeedPermissionsAsync(dbContext, cancellationToken);
    }

    private static async ValueTask SeedUsersAsync(
        ISwagDbContext context,
        SeedOptions seedOptions,
        CancellationToken cancellationToken)
    {
        if (await context.Set<User>().AnyAsync(f => f.Login == seedOptions.AdminLogin, cancellationToken))
            return;

        var hasher = new PasswordHasher<User>();
        var superAdminUser = new User
        {
            Login = seedOptions.AdminLogin,
            RoleId = AdminRoleID
        };
        superAdminUser.PasswordHash = hasher.HashPassword(superAdminUser, seedOptions.AdminPassword);

        await context.Set<User>().AddAsync(superAdminUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        AdminUserID = superAdminUser.Id;
    }

    private static async ValueTask SeedRolesAsync(ISwagDbContext context, CancellationToken cancellationToken)
    {
        if (await context.Set<Role>().AnyAsync(cancellationToken))
            return;

        var superAdminRole = new Role
        {
            Name = "Админ",
            UserType = RoleType.Admin,
        };
        await context.Set<Role>().AddAsync(superAdminRole, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        AdminRoleID = superAdminRole.Id;
    }

    private static async ValueTask SeedPermissionsAsync(ISwagDbContext context, CancellationToken cancellationToken)
    {
        var objectTypes = await context.Set<ObjectType>().AsNoTracking().ToArrayAsync(cancellationToken);
        foreach (var objectType in objectTypes)
        {
            if (await context.Set<Permission>().AnyAsync(p => p.ObjectType == objectType.Name, cancellationToken))
                continue;

            foreach (var action in Enum.GetValues<ActionType>())
            {
                var permission = new Permission
                {
                    ObjectType = objectType.Name,
                    Name = $"{action.GetDescription()} {objectType.NormalizedName}",
                    ActionType = action,
                    RolePermissions = new List<RolePermission>
                    {
                        new()
                        {
                            RoleId = AdminRoleID,
                        },
                    },
                };
                await context.Set<Permission>().AddAsync(permission, cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
