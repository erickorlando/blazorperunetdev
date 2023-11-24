namespace ECommerceWeb.Shared;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public decimal PrecioUnitario { get; set; }
    public string Marca { get; set; } = default!;
    public string Categoria { get; set; } = default!;
    public string? UrlImagen { get; set; }

    public int MarcaId { get; set; }
    public int CategoriaId { get; set; }
}