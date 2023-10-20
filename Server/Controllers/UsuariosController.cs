using ECommerceWeb.Server.Services;
using ECommerceWeb.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsuariosController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(IUserService service, ILogger<UsuariosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // POST: api/Usuarios/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginDtoRequest request)
    {
        var response = await _service.LoginAsync(request);

        _logger.LogInformation("Se inicio sesion desde {RequestID}", HttpContext.Connection.Id);

        return response.Exito ? Ok(response) : Unauthorized(response);
    }

    // POST: api/Usuarios/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegistrarUsuarioDto request)
    {
        var response = await _service.RegisterAsync(request);

        return Ok(response);
    }
}