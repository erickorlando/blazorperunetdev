using ECommerceWeb.Shared;
using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;

namespace ECommerceWeb.Client.Proxy.Interfaces;

public interface IVentaProxy
{
    Task<BaseResponse> CrearVenta(VentaDto request);

    Task<DashboardDto> MostrarDashboard();
}