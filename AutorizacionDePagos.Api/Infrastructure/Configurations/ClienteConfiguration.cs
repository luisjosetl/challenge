using AutorizacionDePagos.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutorizacionDePagos.Api.Infrastructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Nombre).IsRequired();
            
            builder
                .Property(c => c.TipoCliente)
                .HasConversion(new EnumToStringConverter<TipoClienteEnum>())
                .IsRequired();

            builder.Property(c => c.FechaCreacion).IsRequired();

            builder.Property(c => c.Eliminado).IsRequired();

            builder.HasData(
                new Cliente { Id = new Guid("094532e6-be77-4d5a-93c8-3d54131d4337"), Nombre = "Jose", TipoCliente = TipoClienteEnum.PRIMERO, FechaCreacion = DateTime.Now, Eliminado = false },
                new Cliente { Id = new Guid("585ac58c-35fe-4f19-9b0d-99fad357940a"), Nombre = "Jhon", TipoCliente = TipoClienteEnum.SEGUNDO, FechaCreacion = DateTime.Now, Eliminado = false }
                );
        }
    }
}
