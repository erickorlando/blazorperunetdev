using ECommerceWeb.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceWeb.Server.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder
            .Property(p => p.Nombre)
            .HasMaxLength(200);

        builder
            .Property(p => p.Comentarios)
            .HasMaxLength(500);

        var lista = new List<Categoria>()
        {
            new() { Id = 1, Nombre = "Celulares"},
            new() { Id = 2, Nombre = "Televisores"},
            new() { Id = 3, Nombre = "Electrodomésticos"},
        };

        builder.HasData(lista);
    }
}