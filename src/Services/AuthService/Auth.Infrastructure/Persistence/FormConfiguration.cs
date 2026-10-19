using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctrure.Persistence
{
    internal sealed class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ToTable("forms");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
