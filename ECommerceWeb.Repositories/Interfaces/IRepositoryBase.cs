using System.Linq.Expressions;
using ECommerceWeb.Entities;

namespace ECommerceWeb.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    Task<ICollection<TEntity>> ListAsync();

    Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicado);

    Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicado,
                                              Expression<Func<TEntity, TInfo>> selector,
                                              string relaciones);

    Task<TEntity?> FindAsync(int id);

    Task AddAsync(TEntity entity);

    Task UpdateAsync();

    Task DeleteAsync(int id);
}