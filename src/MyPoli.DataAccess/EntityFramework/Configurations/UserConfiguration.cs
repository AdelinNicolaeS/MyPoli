using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoli.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoli.DataAccess
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("Users");

            //builder.HasKey("Id");

            //builder
            //  .Property(b => b.FirstName)
            //  .HasMaxLength(150)
            //  .IsRequired();

            //builder
            //  .Property(b => b.LastName)
            //  .HasMaxLength(50)
            //  .IsRequired();

            //builder
            //  .Property(b => b.Email)
            //  .HasMaxLength(150)
            //  .IsRequired();

            //builder
            //  .Property(b => b.PasswordHash)
            //  .HasMaxLength(150)
            //  .IsRequired();


            //builder.HasMany(u => u.PersonRoles)
            //    .WithOne(ur => ur.Person)
            //    .HasForeignKey(ur => ur.PersonId);
        }
    }
}
