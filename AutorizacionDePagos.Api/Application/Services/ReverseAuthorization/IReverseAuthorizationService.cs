using AutorizacionDePagos.Api.Domain;
using System.Collections;

namespace AutorizacionDePagos.Api.Application.Services.ReverseAuthorization
{
    public interface IReverseAuthorizationService
    {
        Task ChangeStateToReverseAndSave();
    }
}
