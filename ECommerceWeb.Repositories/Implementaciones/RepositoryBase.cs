﻿using System.Linq.Expressions;
using ECommerceWeb.DataAccess.Data;
using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Repositories.Implementaciones;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly ECommerceDbContext Context;

    protected RepositoryBase(ECommerceDbContext context)
    {
        Context = context;
    }

    public async Task<ICollection<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>()
            .Where(p => p.Estado)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicado)
    {
        return await Context.Set<TEntity>()
            .Where(predicado)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<TInfo>> ListAsync<TInfo>(
        Expression<Func<TEntity, bool>> predicado, 
        Expression<Func<TEntity, TInfo>> selector, 
        string relaciones)
    {
        var collection = Context.Set<TEntity>()
            .Where(predicado)
            .AsQueryable();

        // SELECT DE PRODUCTO, "Categoria,Marca"
        // SELECT DE CLIENTE, "TipoCliente"

        if (!string.IsNullOrEmpty(relaciones))
        {
            foreach (var tabla in relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                collection = collection.Include(tabla);
            }
        }

        return await collection
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var registro = await FindAsync(id);
        if (registro is not null)
        {
            registro.Estado = false;
            await UpdateAsync();
        }
    }
}