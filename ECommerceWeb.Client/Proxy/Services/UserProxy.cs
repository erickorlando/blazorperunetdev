using ECommerceWeb.Client.Proxy.Interfaces;
using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;
using System.Net.Http.Json;

namespace ECommerceWeb.Client.Proxy.Services;

public class UserProxy : IUserProxy
{
    private readonly HttpClient _httpClient;

    public UserProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Usuarios/Login", request);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginDtoResponse>();

        return loginResponse!;
    }

    public async Task Register(RegistrarUsuarioDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Usuarios/Register", request);

        //response.EnsureSuccessStatusCode(); // Solo en caso de que no sepa el error en el backend

        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Exito == false)
                throw new InvalidOperationException(resultado.MensajeError);
        }
        else
            throw new InvalidOperationException(response.ReasonPhrase);
    }
}