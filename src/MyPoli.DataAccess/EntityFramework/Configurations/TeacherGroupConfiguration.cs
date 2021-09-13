using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoli.Entities;

namespace MyPoli.DataAccess.EntityFramework.Configurations
{
    public class TeacherGroupConfiguration : IEntityTypeConfiguration<TeacherGroup>
    {
        public void Configure(EntityTypeBuilder<TeacherGroup> builder)
        {
            builder.HasKey(e => new { e.IdTeacher, e.IdGroup })
                    .HasName("Pk_TeacherGroup");

            builder.ToTable("TeacherGroup");

            builder.HasOne(d => d.IdGroupNavigation)
                .WithMany(p => p.TeacherGroups)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_TeacherGroup_Group");

            builder.HasOne(d => d.IdTeacherNavigation)
                .WithMany(p => p.TeacherGroups)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_TeacherGroup_Teacher");
        }
    }
}
