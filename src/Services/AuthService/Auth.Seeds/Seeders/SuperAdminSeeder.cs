using Application.Interfaces;
using Application.Tools;
using Auth.Seeds.Options;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Seeders;

public static class SuperAdminSeeder
{
    private static int AdminUserID = 1;
    private static int AdminRoleID = 1;
    public static async Task SeedAsync(
        IAuthDbContext dbContext,
        SeedOptions seedOptions,
        CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(dbContext, cancellationToken);
        await SeedUsersAsync(dbContext, seedOptions, cancellationToken);
        await SeedPermissionsAsync(dbContext, cancellationToken);
    }

    private static async ValueTask SeedUsersAsync(
        IAuthDbContext context,
        SeedOptions seedOptions,
        CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(f => f.Login == seedOptions.AdminLogin, cancellationToken))
            return;

        var hasher = new PasswordHasher<User>();
        var superAdminUser = new User
        {
            Login = seedOptions.AdminLogin,
            RoleId = AdminRoleID
        };
        superAdminUser.PasswordHash = hasher.HashPassword(superAdminUser, seedOptions.AdminPassword);

        await context.Users.AddAsync(superAdminUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        AdminUserID = superAdminUser.Id;
    }

    private static async ValueTask SeedRolesAsync(IAuthDbContext context, CancellationToken cancellationToken)
    {
        if (await context.Roles.AnyAsync(cancellationToken))
            return;

        var superAdminRole = new Role
        {
            Name = "Админ",
            UserType = RoleType.Admin,
        };
        await context.Roles.AddAsync(superAdminRole, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        AdminRoleID = superAdminRole.Id;
    }

    private static async ValueTask SeedPermissionsAsync(IAuthDbContext context, CancellationToken cancellationToken)
    {
        var objectTypes = await context.ObjectTypes.AsNoTracking().ToArrayAsync(cancellationToken);
        foreach (var objectType in objectTypes)
        {
            if (await context.Permissions.AnyAsync(p => p.ObjectType == objectType.Name, cancellationToken))
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
                await context.Permissions.AddAsync(permission, cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
