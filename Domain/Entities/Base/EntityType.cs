using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
    public class EntityType : IEntityTypeConfiguration<EntityType>
    {
        public int TypeID { get; set; }

        [StringLength(100)]
        public string ClassName { get; set; }

        [StringLength(180)]
        public string Title { get; set; }

        public void Configure(EntityTypeBuilder<EntityType> builder)
        {
            builder.HasKey(f => f.TypeID);
        }
    }
}