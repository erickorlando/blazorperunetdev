using System.Net.Http.Json;
using ECommerceWeb.Client.Proxy.Interfaces;
using ECommerceWeb.Shared;
using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;

namespace ECommerceWeb.Client.Proxy.Services;

public class VentaProxy : IVentaProxy
{
    private readonly HttpClient _httpClient;

    public VentaProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BaseResponse> CrearVenta(VentaDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Ventas", request);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<BaseResponse>();

            return result!;
        }

        var badResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
        if (badResponse is not null)
        {
            throw new InvalidOperationException(badResponse.MensajeError);
        }

        throw new InvalidOperationException("No se pudo procesar la venta");
    }

    public async Task<DashboardDto> MostrarDashboard()
    {
        var response = await _httpClient.GetAsync("api/Ventas/dashboard");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<DashboardDto>();
            if (result is not null)
                return result;
        }

        throw new InvalidOperationException("No se puede cargar el dashboard");
    }
}