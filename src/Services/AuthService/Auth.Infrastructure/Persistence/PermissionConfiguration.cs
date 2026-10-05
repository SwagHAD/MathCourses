using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Enums;

namespace Infrasctrure.Persistence;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.ObjectType)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne(f => f.ObjectTypeRef)
                .WithMany()
                .HasForeignKey(f => f.ObjectType)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ActionType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => new { x.ObjectType, x.ActionType })
            .IsUnique();
    }
}
