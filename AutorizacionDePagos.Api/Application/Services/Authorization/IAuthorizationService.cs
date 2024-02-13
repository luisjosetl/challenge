using AutorizacionDePagos.Api.Application.Authorization;
using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.Application.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<Autorizacion> Create(CreateAuthorizationRequest request);
    }
}
