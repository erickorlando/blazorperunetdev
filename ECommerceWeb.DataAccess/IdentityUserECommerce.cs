using Microsoft.AspNetCore.Identity;

namespace ECommerceWeb.DataAccess;

public class IdentityUserECommerce : IdentityUser
{
    public string NombreCompleto { get; set; } = default!;

    public DateTime FechaNacimiento { get; set; }

    public string Direccion { get; set; } = default!;
}