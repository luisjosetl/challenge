using AutorizacionDePagos.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizacionDePagos.Api.Infrastructure.Configurations
{
    public class AutorizacionConfiguration : IEntityTypeConfiguration<Autorizacion>
    {
        public void Configure(EntityTypeBuilder<Autorizacion> builder)
        {
            builder.ToTable("Autorizaciones");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.FechaCreacion).IsRequired();

            builder.Property(a => a.Eliminado).IsRequired();
            
            builder.Property(a => a.NombreCliente).IsRequired();

            builder.Property(a => a.CodigoOperacion).IsRequired();
            
            builder.Property(a => a.Monto).IsRequired(false);
            
            builder.Property(a => a.MontoDenegado)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(false);

            builder.HasOne(a => a.Estado)
                .WithMany()
                .HasForeignKey(a => a.EstadoAutorizacionId);
        }
    }
}
