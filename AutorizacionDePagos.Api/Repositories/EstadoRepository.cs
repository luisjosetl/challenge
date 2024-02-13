using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutorizacionDePagos.Api.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Estado> dbSet;

        public EstadoRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Estado>();
        }

        public async Task<Estado> GetStateByName(EstadoEnum estado)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Nombre == estado.ToString());
        }
    }
}
