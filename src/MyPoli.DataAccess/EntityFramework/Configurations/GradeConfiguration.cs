using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoli.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyPoli.DataAccess
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(e => new { e.IdStudent, e.IdSubject })
                     .HasName("Pk_Grade");

            builder.ToTable("Grade");

            builder.Property(e => e.GradeValue).HasColumnName("Grade");
        }
    }
}
