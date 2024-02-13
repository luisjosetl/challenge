namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class AuthorizationResponseDto
    {
        public Guid Id { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoOperacion { get; set; }
        public int? Monto { get; set; }
        public decimal? MontoDenegado { get; set; }
        public string TipoAutorizacion { get; set; }
        public string Estado { get; set; }
    }
}
