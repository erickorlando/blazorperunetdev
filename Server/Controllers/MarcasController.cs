using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly IMarcaRepository _repository;

    public MarcasController(IMarcaRepository repository)
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
    [Authorize]
    public async Task<IActionResult> Post(Marca request)
    {
        await _repository.AddAsync(request);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Marca request)
    {
        var registro = await _repository.FindAsync(id);

        if (registro is null)
        {
            return NotFound();
        }

        registro.Nombre = request.Nombre;
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