using AutorizacionDePagos.Api.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AutorizacionDePagos.Api.Infrastructure.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new Estado { Id = Guid.NewGuid(), Nombre = "CONFIRMAR" },
                new Estado { Id = Guid.NewGuid(), Nombre = "APROBADO" },
                new Estado { Id = Guid.NewGuid(), Nombre = "DENEGADO" },
                new Estado { Id = Guid.NewGuid(), Nombre = "NO_CONFIRMADO" }
                );
        }
    }
}
