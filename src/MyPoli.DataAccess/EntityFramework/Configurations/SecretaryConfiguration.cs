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
    public class SecretaryConfiguration : IEntityTypeConfiguration<Secretary>
    {
        public void Configure(EntityTypeBuilder<Secretary> builder)
        {
            builder.ToTable("Secretary");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Salary).HasColumnType("decimal(15, 2)");

            builder.HasOne(d => d.Person)
                .WithOne(p => p.Secretary)
                .HasForeignKey<Secretary>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SecretaryPerson");
        }
    }
}
