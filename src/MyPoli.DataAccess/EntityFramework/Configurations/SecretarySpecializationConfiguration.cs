using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPoli.Entities;

namespace MyPoli.DataAccess.EntityFramework.Configurations
{
    public class SecretarySpecializationConfiguration : IEntityTypeConfiguration<SecretarySpecialization>
    {
        public void Configure(EntityTypeBuilder<SecretarySpecialization> builder)
        {
            builder.HasKey(e => new { e.IdSecretary, e.IdSpecialization })
                    .HasName("Pk_SecretarySpecialization");

            builder.ToTable("SecretarySpecialization");

            builder.HasOne(d => d.IdSecretaryNavigation)
                .WithMany(p => p.SecretarySpecializations)
                .HasForeignKey(d => d.IdSecretary)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SecretarySpecialization_Secretary");

            builder.HasOne(d => d.IdSpecializationNavigation)
                .WithMany(p => p.SecretarySpecializations)
                .HasForeignKey(d => d.IdSpecialization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SecretarySpecialization_Specialization");
        }
    }
}
