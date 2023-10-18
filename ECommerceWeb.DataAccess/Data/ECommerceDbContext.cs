using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ECommerceWeb.DataAccess.Data;

public class ECommerceDbContext : IdentityDbContext<IdentityUserECommerce>
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // AspNetUsers
        modelBuilder.Entity<IdentityUserECommerce>(e =>
        {
            e.ToTable("Usuario");
        });

        // AspNetRoles
        modelBuilder.Entity<IdentityRole>(e =>
        {
            e.ToTable("Rol");
        });

        // AspNetUserRoles
        modelBuilder.Entity<IdentityUserRole<string>>(e =>
        {
            e.ToTable("UsuarioRol");
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        //configurationBuilder.Properties<string>().HaveMaxLength(100); // ESTO ES UN EJEMPLO
        configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
    }
}