using ECommerceWeb.Shared;

namespace ECommerceWeb.Client.Proxy.Interfaces;

public interface IProductoProxy
{
    Task<ICollection<ProductoDto>> ListAsync(string filtro);
}