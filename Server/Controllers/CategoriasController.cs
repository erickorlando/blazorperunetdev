using ECommerceWeb.Server.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly List<Categoria> _list;

    public CategoriasController()
    {
        _list = new List<Categoria>()
        {
            new() { Id = 1, Nombre = "Celulares"},
            new() { Id = 2, Nombre = "Televisores"},
            new() { Id = 3, Nombre = "Electrodomésticos"},
        };
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await Task.FromResult(_list)); //Carga la lista inicializada
    }
}