using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name)
                .HasMaxLength(50);
            builder.Property(f => f.Surname)
                .HasMaxLength(50);
            builder.Property(f => f.Patronymic)
                .HasMaxLength(50);
            builder.Property(f => f.Email)
                .HasMaxLength(100);
            builder.Property(f => f.Phone)
                .HasMaxLength(50);
            builder.HasIndex(f => f.UserId)
                .IsUnique();
        }
    }
}
