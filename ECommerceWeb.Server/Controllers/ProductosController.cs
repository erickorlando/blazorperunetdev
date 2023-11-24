using AutoMapper;
using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using ECommerceWeb.Server.Services;
using ECommerceWeb.Shared;
using ECommerceWeb.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;

    public ProductosController(IProductoRepository repository, IMapper mapper, IFileUploader fileUploader)
    {
        _repository = repository;
        _mapper = mapper;
        _fileUploader = fileUploader;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? filtro)
    {
        var lista = await _repository.ListAsync(
            predicado: p => p.Estado && p.Nombre.Contains(filtro ?? string.Empty),
           selector: x => new ProductoDto
           {
               Id = x.Id,
               Nombre = x.Nombre,
               PrecioUnitario = x.PrecioUnitario,
               Marca = x.Marca.Nombre,
               Categoria = x.Categoria.Nombre, 
               CategoriaId = x.CategoriaId,
               UrlImagen = x.UrlImagen
           }, relaciones: "Marca,Categoria");

        return Ok(lista);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _repository.FindAsync(id);

        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductoDtoRequest request)
    {
        var producto = _mapper.Map<Producto>(request);

        producto.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.NombreArchivo);

        await _repository.AddAsync(producto);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ProductoDtoRequest request)
    {
        var registro = await _repository.FindAsync(id);
        if (registro == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(request.Base64Imagen))
        {
            request.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.NombreArchivo);
        }

        _mapper.Map(request, registro);

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