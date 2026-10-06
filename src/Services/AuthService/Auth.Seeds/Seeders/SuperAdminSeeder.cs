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
}
