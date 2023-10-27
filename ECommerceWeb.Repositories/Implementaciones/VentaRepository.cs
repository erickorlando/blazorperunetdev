using System.Data;
using Dapper;
using ECommerceWeb.DataAccess.Data;
using ECommerceWeb.Entities;
using ECommerceWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Repositories.Implementaciones;

public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
{
    public VentaRepository(ECommerceDbContext context) 
        : base(context)
    {
    }

    public override async Task AddAsync(Venta entity)
    {
        await Context.AddAsync(entity);
    }

    public async Task CrearTransaccionAsync()
    {
        await Context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
    }

    public async Task ConfirmarTransaccionAsync()
    {
        await Context.Database.CommitTransactionAsync();
        await Context.SaveChangesAsync();
    }

    public async Task ResetearTransaccionAsync()
    {
        await Context.Database.RollbackTransactionAsync();
    }

    public async Task<Dashboard> MostrarDashboard()
    {
        var query = await Context.Database.GetDbConnection()
            .QuerySingleAsync<Dashboard>("uspDashBoard", commandType: CommandType.StoredProcedure);

        return query;
    }
}