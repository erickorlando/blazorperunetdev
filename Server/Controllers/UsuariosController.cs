using ECommerceWeb.Server.Services;
using ECommerceWeb.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsuariosController : ControllerBase
{
    private readonly IUserService _service;

    public UsuariosController(IUserService service)
    {
        _service = service;
    }

    // POST: api/Usuarios/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginDtoRequest request)
    {
        var response = await _service.LoginAsync(request);

        return response.Exito ? Ok(response) : Unauthorized(response);
    }

    // POST: api/Usuarios/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegistrarUsuarioDto request)
    {
        var response = await _service.RegisterAsync(request);

        return response.Exito ? Ok(response) : BadRequest(response);
    }
}