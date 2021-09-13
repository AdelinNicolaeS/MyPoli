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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Address).IsRequired();

            builder.Property(e => e.Birthday).HasColumnType("date");

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(d => d.Gender)
                .WithMany(p => p.People)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Gender_Person");

            builder.HasOne(d => d.Nationality)
                .WithMany(p => p.People)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Nationality_Person");
        }
    }
}
