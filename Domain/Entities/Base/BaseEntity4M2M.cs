using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Base
{
    /// <summary>
    /// Базовый класс для промежуточных сущностей "многие-ко-многим"
    /// </summary>
    /// <typeparam name="TEntity1">Первая сущность (должна наследовать BaseEntity)</typeparam>
    /// <typeparam name="TEntity2">Вторая сущность (должна наследовать BaseEntity)</typeparam>
    public abstract class BaseEntity4M2M<TEntity1, TEntity2> : IEntityTypeConfiguration<BaseEntity4M2M<TEntity1, TEntity2>>
        where TEntity1 : BaseEntity
        where TEntity2 : BaseEntity
    {
        public int TEntity1Id { get; set; }
        public TEntity1 Entity1 { get; set; }

        public int TEntity2Id { get; set; }
        public TEntity2 Entity2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        protected abstract void CustomCofigure(EntityTypeBuilder<BaseEntity4M2M<TEntity1, TEntity2>> builder);

        public void Configure(EntityTypeBuilder<BaseEntity4M2M<TEntity1, TEntity2>> builder)
        {

            builder.HasKey(x => new { x.TEntity1Id, x.TEntity2Id });

            builder.HasOne(x => x.Entity1)
                .WithMany()
                .HasForeignKey(x => x.TEntity1Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Entity2)
                .WithMany()
                .HasForeignKey(x => x.TEntity2Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(x => new { x.TEntity1Id, x.TEntity2Id})
                .IsUnique();

            CustomCofigure(builder);
        }
    }
}