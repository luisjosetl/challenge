namespace AutorizacionDePagos.Api.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime FechaCreacion { get; set; }

        DateTime? FechaModificacion { get; set; }

        bool Eliminado { get; set; }
    }
}
