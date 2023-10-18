using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceWeb.DataAccess.Configurations;

public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUserECommerce>
{
    public void Configure(EntityTypeBuilder<IdentityUserECommerce> builder)
    {
        builder.Property(p => p.NombreCompleto)
            .HasMaxLength(200);

        builder.Property(p => p.FechaNacimiento)
            .HasColumnType("DATE");

        builder.Property(p => p.Direccion)
            .HasMaxLength(500);
    }
}