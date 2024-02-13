using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.Interfaces
{
    public interface ITipoAutorizacionRepository
    {
        Task<TipoAutorizacion> GetAuthorizationTypeByName(TipoAutorizacionEnum tipoAutorizacion);
        Task<TipoAutorizacion> GetById(Guid id);
        Task<List<TipoAutorizacion>> GetAllList();
    }
}
