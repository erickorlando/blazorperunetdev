namespace ECommerceWeb.Shared.Request;

public class ProductoDtoRequest
{
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public decimal PrecioUnitario { get; set; }
    public int MarcaId { get; set; }
    public int CategoriaId { get; set; }

    public string? Base64Imagen { get; set; }
    public string? NombreArchivo { get; set; }
    public string? UrlImagen { get; set; }
}