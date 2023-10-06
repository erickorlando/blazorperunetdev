using ECommerceWeb.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Server.Data;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
        :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>()
            .HasKey(p => p.Id); // Esta es la llave primaria

        modelBuilder.Entity<Categoria>()
            .Property(p => p.Nombre)
            .HasMaxLength(200);

        modelBuilder.Entity<Categoria>()
            .Property(p => p.Comentarios)
            .HasMaxLength(500);

        var lista  = new List<Categoria>()
        {
            new() { Id = 1, Nombre = "Celulares"},
            new() { Id = 2, Nombre = "Televisores"},
            new() { Id = 3, Nombre = "Electrodomésticos"},
        };

        modelBuilder.Entity<Categoria>()
            .HasData(lista);
    }
}