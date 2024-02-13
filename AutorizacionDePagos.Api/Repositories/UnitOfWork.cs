using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;

namespace AutorizacionDePagos.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
