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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.StartDate).HasColumnType("date");

            builder.HasOne(d => d.Group)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_StudentGroup");

            builder.HasOne(d => d.Person)
                .WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentPerson");

            builder.HasOne(d => d.Status)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("Fk_Status_Student");
        }
    }
}
