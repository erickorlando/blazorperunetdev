using System.Security.Claims;
using AutoMapper;
using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using ECommerceWeb.Shared;
using ECommerceWeb.Shared.Request;
using ECommerceWeb.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<VentasController> _logger;
    private readonly IClienteRepository _clienteRepository;

    public VentasController(IVentaRepository repository, IMapper mapper, ILogger<VentasController> logger, 
        IClienteRepository clienteRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _clienteRepository = clienteRepository;
    }

    [HttpPost]
    [Authorize(Roles = Constantes.RolCliente)]
    public async Task<IActionResult> Post([FromBody] VentaDto request)
    {
        var response = new BaseResponse();

        try
        {
            // Buscamos el ID del Cliente basado en el correo, y el correo sale de la autenticacion
            var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            var cliente = await _clienteRepository.BuscarPorEmailAsync(email);

            if (cliente is null)
            {
                response.MensajeError = $"El cliente con el correo {email} no existe!";
                return BadRequest(response);
            }

            request.ClienteId = cliente.Id;

            var venta = _mapper.Map<Venta>(request);
            await _repository.CrearTransaccionAsync();
            await _repository.AddAsync(venta);

            await _repository.ConfirmarTransaccionAsync();

            _logger.LogInformation("Se creo la venta de forma correcta");

            response.Exito = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.MensajeError = "Error al crear la venta";
            await _repository.ResetearTransaccionAsync();
            _logger.LogCritical(ex, "{MensajeError} {Message}", response.MensajeError, ex.Message);
            return BadRequest(response);
        }
    }

    [HttpGet("dashboard")]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var response = await _repository.MostrarDashboard();

        return Ok(response);
    }
}