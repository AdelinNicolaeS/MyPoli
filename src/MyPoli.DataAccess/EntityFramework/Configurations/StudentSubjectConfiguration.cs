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
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(e => new { e.IdStudent, e.IdSubject })
                     .HasName("Pk_StudentSubject");

            builder.ToTable("StudentSubject");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_StudentSubject_Student");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_StudentSubject_Subject");

            builder.HasOne(d => d.Grade)
                .WithOne(p => p.StudentSubject)
                .HasForeignKey<StudentSubject>(d => new { d.IdStudent, d.IdSubject })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_StudentSubject_Grade");
        }
    }
}
