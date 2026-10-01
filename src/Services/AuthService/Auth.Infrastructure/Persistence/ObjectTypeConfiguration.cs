using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctrure.Persistence
{
    internal sealed class ObjectTypeConfiguration : IEntityTypeConfiguration<ObjectType>
    {
        public void Configure(EntityTypeBuilder<ObjectType> builder)
        {
            builder.ToTable("sys_ObjectTypes");

            builder.HasKey(x => x.Name);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.NormalizedName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.ServiceType)
                .IsRequired();

            builder.HasIndex(x => new { x.Name, x.ServiceType })
                .IsUnique();
        }
    }
}
