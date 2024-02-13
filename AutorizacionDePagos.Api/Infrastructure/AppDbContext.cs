using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AutorizacionDePagos.Api.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Autorizacion> Autorizacion { get; set; }
        public DbSet<AutorizacionAprobada> AutorizacionAprobada { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<TipoAutorizacion> TipoAutorizacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorizacionAprobadaConfiguration());
            modelBuilder.ApplyConfiguration(new AutorizacionConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EstadoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoAutorizacionConfiguration());
        }
    }
}
