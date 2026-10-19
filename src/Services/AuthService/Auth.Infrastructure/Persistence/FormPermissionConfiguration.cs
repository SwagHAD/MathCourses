using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctrure.Persistence
{
    internal sealed class FormPermissionConfiguration : IEntityTypeConfiguration<FormPermission>
    {
        public void Configure(EntityTypeBuilder<FormPermission> builder)
        {
            builder.ToTable("formPermissions");
            builder.HasKey(x => x.Id);
            builder.HasOne(f => f.FormRef).WithMany()
                .HasForeignKey(f => f.FormId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => new { x.FormId, x.ActionType })
                .IsUnique();
        }
    }
}
