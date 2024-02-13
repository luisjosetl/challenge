using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization
{
    public interface IApprovedAuthorizationService
    {
        Task ApprovedAuthorizationPublish(Autorizacion authorization);

        Task SaveApprovedAuthorization(AutorizacionAprobada approvedAuthorization);
    }
}
