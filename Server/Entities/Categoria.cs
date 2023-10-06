namespace ECommerceWeb.Server.Entities;

// POCO: PLAIN OLD CLR (COMMON LANGUAGE RUNTIME) OBJECT
public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string? Comentarios { get; set; }
}