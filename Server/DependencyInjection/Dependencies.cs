using ECommerceWeb.Repositories.Implementaciones;
using ECommerceWeb.Repositories.Interfaces;
using ECommerceWeb.Server.Perfiles;
using ECommerceWeb.Server.Services;

namespace ECommerceWeb.Server.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICategoriaRepository, CategoriaRepository>()
                .AddTransient<IMarcaRepository, MarcaRepository>()
                .AddTransient<IProductoRepository, ProductoRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddTransient<IFileUploader, FileUploader>()
            .AddTransient<IUserService, UserService>();
    }

    public static IServiceCollection AddAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<ProductoProfile>();
        });
        return services;
    }
}