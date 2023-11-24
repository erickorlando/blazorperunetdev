using AutoMapper;
using ECommerceWeb.Entities;
using ECommerceWeb.Shared.Request;

namespace ECommerceWeb.Server.Perfiles;

public class ProductoProfile : Profile
{
    public ProductoProfile()
    {
        // Origen -> Destino
        CreateMap<ProductoDtoRequest, Producto>();
    }
}