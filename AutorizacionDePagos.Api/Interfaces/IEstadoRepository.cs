using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.Interfaces
{
    public interface IEstadoRepository
    {
        Task<Estado> GetStateByName(EstadoEnum estado);
    }
}
