namespace ECommerceWeb.Shared;

public class CarritoDto
{
    public ProductoDto ProductoDto { get; set; } = default!;

    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
}