using System.Net.Http.Json;
using ECommerceWeb.Client.Proxy.Interfaces;
using ECommerceWeb.Shared;
using ECommerceWeb.Shared.Request;

namespace ECommerceWeb.Client.Proxy.Services;

public class ProductoProxy : IProductoProxy
{
    private readonly HttpClient _httpClient;

    public ProductoProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ICollection<ProductoDto>> ListAsync(string filtro)
    {
        var response = await _httpClient.GetAsync($"api/Productos?filtro={filtro}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ICollection<ProductoDto>>();

        if (result != null)
        {
            return result;
        }

        throw new InvalidOperationException(response.ReasonPhrase);
    }

    public async Task<ProductoDto?> FindByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/Productos/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductoDto>();
    }
}