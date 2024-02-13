using AutorizacionDePagos.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizacionDePagos.Api.Infrastructure.Configurations
{
    public class TipoAutorizacionConfiguration : IEntityTypeConfiguration<TipoAutorizacion>
    {
        public void Configure(EntityTypeBuilder<TipoAutorizacion> builder)
        {
            builder.ToTable("TiposAutorizacion");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new TipoAutorizacion { Id = Guid.NewGuid(), Nombre = "COBRO" },
                new TipoAutorizacion { Id = Guid.NewGuid(), Nombre = "DEVOLUCION" },
                new TipoAutorizacion { Id = Guid.NewGuid(), Nombre = "REVERSA" }
                );
        }
    }
}
