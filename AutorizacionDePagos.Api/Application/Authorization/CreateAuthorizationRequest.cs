using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class CreateAuthorizationRequest
    {
        public string Monto { get; set; }

        public string CodigoOperacion { get; set; }

        public TipoAutorizacionEnum TipoAutorizacion { get; set; }

        public Guid ClienteId { get; set; }
    }
}
