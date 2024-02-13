using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutorizacionDePagos.Api.Repositories
{
    public class AutorizacionRepository : IAutorizacionRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Autorizacion> dbSet;

        public AutorizacionRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Autorizacion>();
        }

        public async Task<List<Autorizacion>> GetAutorizacionesForReversing(int minutesLimit)
        {
            try
            {
                var timeLimit = DateTime.Now - TimeSpan.FromMinutes(minutesLimit);

                var authorizations = await dbSet
                .Include(a => a.Estado)
                .Where(a =>
                    a.Estado.Nombre == EstadoEnum.CONFIRMAR.ToString() &&
                    a.FechaCreacion < timeLimit)
                .ToListAsync();

                return authorizations;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<List<Autorizacion>> GetAllAutorizaciones()
        {
            try
            {
                var authorizations = await dbSet
                    .Include(a => a.Estado)
                    .Include(a => a.TipoAutorizacion)
                    .ToListAsync();

                return authorizations;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Autorizacion> GetAutorizacionesById(Guid id)
        {
            try
            {
                var authorization = await dbSet
                    .Include(a => a.Estado)
                    .Include(a => a.TipoAutorizacion)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return authorization;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
