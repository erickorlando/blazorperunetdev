using ECommerceWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceWeb.DataAccess.Configurations;

public class VentaDetalleConfiguration : IEntityTypeConfiguration<VentaDetalle>
{
    public void Configure(EntityTypeBuilder<VentaDetalle> builder)
    {
        builder.Property(p => p.Precio)
            .HasPrecision(11, 2);
        
        builder.Property(p => p.Total)
            .HasPrecision(11, 2);
    }
}