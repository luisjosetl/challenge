using AutorizacionDePagos.Api.Interfaces;

namespace AutorizacionDePagos.Api.Domain
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Eliminado { get; set; }
    }
}
