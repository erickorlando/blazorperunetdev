namespace ECommerceWeb.Shared.Request;

public class VentaDto
{
    public int ClienteId { get; set; }
    public decimal Total { get; set; }
    public ICollection<VentaDetalleDto> VentaDetalles { get; set; } = new List<VentaDetalleDto>();
}

public class VentaDetalleDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
}