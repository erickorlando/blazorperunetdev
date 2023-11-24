using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriasController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.ListAsync()); //Carga la lista inicializada
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _repository.FindAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Categoria request)
    {
        await _repository.AddAsync(request);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Categoria request)
    {
        var registro = await _repository.FindAsync(id);

        if (registro is null)
        {
            return NotFound();
        }

        registro.Nombre = request.Nombre;
        registro.Comentarios = request.Comentarios;
        await _repository.UpdateAsync();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return Ok();
    }
}