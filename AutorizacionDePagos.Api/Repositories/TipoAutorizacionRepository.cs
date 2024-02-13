using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutorizacionDePagos.Api.Repositories
{
    public class TipoAutorizacionRepository : ITipoAutorizacionRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<TipoAutorizacion> dbSet;

        public TipoAutorizacionRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TipoAutorizacion>();
        }

        public async Task<TipoAutorizacion> GetAuthorizationTypeByName(TipoAutorizacionEnum tipoAutorizacion)
        {
            return await dbSet.FirstOrDefaultAsync(t => t.Nombre == tipoAutorizacion.ToString());
        }

        public async Task<TipoAutorizacion> GetById(Guid id)
        {
            return await dbSet.FirstAsync(t => t.Id == id);
        }

        public async Task<List<TipoAutorizacion>> GetAllList()
        {
            return await dbSet.ToListAsync();
        }
    }
}
