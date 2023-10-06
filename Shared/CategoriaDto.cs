using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Shared;

public class CategoriaDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(20)]
    public string Nombre { get; set; } = default!;
    public string? Comentarios { get; set; }
}