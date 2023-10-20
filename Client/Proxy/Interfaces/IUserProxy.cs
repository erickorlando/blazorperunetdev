using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;

namespace ECommerceWeb.Client.Proxy.Interfaces;

public interface IUserProxy
{
    Task<LoginDtoResponse> Login(LoginDtoRequest request);

    Task Register(RegistrarUsuarioDto request);
}