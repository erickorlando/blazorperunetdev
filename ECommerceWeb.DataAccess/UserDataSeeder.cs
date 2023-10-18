using ECommerceWeb.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceWeb.DataAccess;

public static class UserDataSeeder
{
    public static async Task Seed(IServiceProvider service)
    {
        // UserManager (Repositorio de Usuarios)
        var userManager = service.GetRequiredService<UserManager<IdentityUserECommerce>>();
        // RoleManager (Repositorio de Roles)
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear roles
        var adminRole = new IdentityRole(Constantes.RolAdministrador);
        var clienteRole = new IdentityRole(Constantes.RolCliente);

        await roleManager.CreateAsync(adminRole);

        await roleManager.CreateAsync(clienteRole);

        // Usuario Administrador
        var adminUser = new IdentityUserECommerce()
        {
            NombreCompleto = "Administrador del sistema",
            FechaNacimiento = DateTime.Parse("1990-01-01"),
            Direccion = "Av. Siempre viva 123",
            UserName = "admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "pa$$W0rD@123");
        if (result.Succeeded)
        {
            // Esto me asegura que el usuario se creo correctamente
            adminUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (adminUser is not null)
                await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
        }
    }
}