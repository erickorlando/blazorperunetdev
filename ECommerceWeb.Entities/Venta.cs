namespace ECommerceWeb.Entities;

public class Venta : EntityBase
{
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; }
    // No se va a mapear en el EF Core
    public ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();

    public Venta()
    {
        FechaCreacion = DateTime.Now;
    }
}

public class VentaDetalle : EntityBase
{
    public Venta Venta { get; set; } = default!;
    public int VentaId { get; set; }

    public Producto Producto { get; set; } = default!;
    public int ProductoId { get; set; }

    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}