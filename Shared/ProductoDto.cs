namespace ECommerceWeb.Shared;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public decimal PrecioUnitario { get; set; }
    public string Marca { get; set; } = default!;
    public string Categoria { get; set; } = default!;
}