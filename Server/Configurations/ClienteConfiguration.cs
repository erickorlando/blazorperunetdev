using ECommerceWeb.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceWeb.Server.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.Property(p => p.Nombres)
            .HasMaxLength(200);
        
        builder.Property(p => p.Apellidos)
            .HasMaxLength(200);
        
        builder.Property(p => p.Email)
            .IsUnicode(false)
            .HasMaxLength(500);

        builder.Property(p => p.FechaNacimiento)
            .HasColumnType("DATE");
    }
}