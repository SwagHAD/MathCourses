using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public abstract class BaseEntity<TEntity> : BaseEntity, IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TEntity>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");
            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);
            CustomConfigure(builder);
        }

        protected abstract void CustomConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
