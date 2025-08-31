﻿using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Students")]
    public class Student : BaseEntity<Student>
    {
        public string Name { get; set; }

        public List<StudentGroup> StudentGroups { get; set; } = new();
        protected override void CustomConfigure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired().HasMaxLength(100);
        }
    }
}
