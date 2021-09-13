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
    public class SubjectTeacherConfiguration : IEntityTypeConfiguration<SubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectTeacher> builder)
        {
            builder.HasKey(e => new { e.TeacherId, e.SubjectId })
                    .HasName("Pk_SubjectTeacher");

            builder.ToTable("SubjectTeacher");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.SubjectTeachers)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectTeacher_Subject");

            builder.HasOne(d => d.Teacher)
                .WithMany(p => p.SubjectTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectTeacher_Teacher");
        }
    }
}
