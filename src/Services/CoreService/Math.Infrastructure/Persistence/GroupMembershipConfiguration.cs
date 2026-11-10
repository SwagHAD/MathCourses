using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class GroupMembershipConfiguration : IEntityTypeConfiguration<GroupMembership>
    {
        public void Configure(EntityTypeBuilder<GroupMembership> builder)
        {
            builder.ToTable("GroupMemberships");
            // Человек в группе один раз: либо учится, либо ведёт
            builder.HasKey(m => new { m.GroupId, m.PersonId });
            builder.Property(m => m.Kind).HasConversion<string>().HasMaxLength(20);
            builder.Property(m => m.Status).HasConversion<string>().HasMaxLength(20);
            // Состав группы удаляется вместе с группой
            builder.HasOne(m => m.GroupRef).WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PersonRef).WithMany(p => p.Memberships)
                .HasForeignKey(m => m.PersonId).OnDelete(DeleteBehavior.Restrict);
            // Обратный поиск «в каких группах человек» — основной запрос проверки доступа
            builder.HasIndex(m => new { m.PersonId, m.Status });
        }
    }
}
