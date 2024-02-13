using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutorizacionDePagos.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.Eliminado);
        }

        public async Task AddAsync(T entity)
        {
            entity.FechaCreacion = DateTime.Now;
            entity.Eliminado = false;
            await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    entity.FechaCreacion = DateTime.Now;
                    entity.Eliminado = false;
                }
            }
            
            await dbSet.AddRangeAsync(entities);
        }

        public void UpdateAsync(T entity)
        {
            entity.FechaModificacion = DateTime.Now;
            dbSet.Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            entity.Eliminado = true;
            dbSet.Update(entity);
        }

        public async Task<List<T>> GetAllListAsync()
        {
            var result = await dbSet.Where(x => !x.Eliminado).ToListAsync();
            return result;
        }

        public async Task<List<T>> GetListByConditionAsync(Expression<Func<T, bool>> condition)
        {
            var result = await dbSet.Where(condition).ToListAsync();
            result.Where(x => !x.Eliminado);
            return result;
        }
    }
}
