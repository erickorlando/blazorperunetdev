using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Shared;

public class MarcaDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = default!;
}