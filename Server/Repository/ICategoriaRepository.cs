using ECommerceWeb.Server.Entities;

namespace ECommerceWeb.Server.Repository;

public interface ICategoriaRepository
{
    Task<ICollection<Categoria>> ListAsync();

    Task<Categoria?> FindAsync(int id);

    Task AddAsync(Categoria entity);

    Task UpdateAsync(int id, Categoria entity);

    Task DeleteAsync(int id);
}