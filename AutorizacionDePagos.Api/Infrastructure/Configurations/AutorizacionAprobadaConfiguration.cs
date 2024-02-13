using AutorizacionDePagos.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizacionDePagos.Api.Infrastructure.Configurations
{
    public class AutorizacionAprobadaConfiguration : IEntityTypeConfiguration<AutorizacionAprobada>
    {
        public void Configure(EntityTypeBuilder<AutorizacionAprobada> builder)
        {
            builder.ToTable("AutorizacionesAprobadas");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.FechaCreacion).IsRequired();

            builder.Property(a => a.Eliminado).IsRequired();

        }
    }
}
