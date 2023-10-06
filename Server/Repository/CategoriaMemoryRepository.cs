using ECommerceWeb.Server.Entities;

namespace ECommerceWeb.Server.Repository;

public class CategoriaMemoryRepository : ICategoriaRepository
{
    private readonly List<Categoria> _list;
    public CategoriaMemoryRepository()
    {
        _list = new List<Categoria>()
        {
            new() { Id = 1, Nombre = "Celulares de Alta Gama", Comentarios = "solo los mas caros" },
            new() { Id = 2, Nombre = "Celulares de Gama media", Comentarios = "para todos los bolsillos" },
        };
    }

    public async Task<ICollection<Categoria>> ListAsync()
    {
        return await Task.FromResult(_list);
    }

    public Task<Categoria?> FindAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Categoria entity)
    {
        entity.Id = _list.Count + 1;
        _list.Add(entity);
        await Task.FromResult(0);
    }

    public Task UpdateAsync(int id, Categoria entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}