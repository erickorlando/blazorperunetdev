using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Shared.Request;

public class RegistrarUsuarioDto
{
    [Required]
    public string NombreCompleto { get; set; } = default!;

    public DateTime FechaNacimiento { get; set; } = DateTime.Today.AddYears(-20);

    [Required]
    public string Direccion { get; set; } = default!;

    [Required]
    public string NombreUsuario { get; set; } = default!;

    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = default!;
}