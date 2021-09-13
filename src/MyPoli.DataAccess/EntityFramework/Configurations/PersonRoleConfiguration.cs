using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoli.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoli.DataAccess
{
    public class PersonRoleConfiguration : IEntityTypeConfiguration<PersonRole>
    {
        public void Configure(EntityTypeBuilder<PersonRole> builder)
        {
            builder.ToTable("PersonRoles");

            builder.HasKey(ur => new { ur.RoleId, ur.PersonId });
        }
    }
}
