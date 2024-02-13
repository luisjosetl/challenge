using AutorizacionDePagos.Api.Domain;
using System.Linq.Expressions;

namespace AutorizacionDePagos.Api.Interfaces
{
    public interface IAutorizacionRepository
    {
        Task<List<Autorizacion>> GetAutorizacionesForReversing(int minutesLimit);
        Task<List<Autorizacion>> GetAllAutorizaciones();
        Task<Autorizacion> GetAutorizacionesById(Guid id);
    }
}
