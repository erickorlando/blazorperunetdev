using ECommerceWeb.Repositories.Interfaces;
using ECommerceWeb.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repository;

    public ProductosController(IProductoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _repository.ListAsync(p => p.Estado, x => new ProductoDto
        {
            Id = x.Id,
            Nombre = x.Nombre,
            PrecioUnitario = x.PrecioUnitario,
            Marca = x.Marca.Nombre,
            Categoria = x.Categoria.Nombre
        }, "Marca,Categoria");

        return Ok(lista);
    }
}