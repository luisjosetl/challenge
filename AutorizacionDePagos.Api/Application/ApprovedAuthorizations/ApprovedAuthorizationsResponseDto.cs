namespace AutorizacionDePagos.Api.Application.ApprovedAuthorizations
{
    public class ApprovedAuthorizationsResponseDto
    {
        public Guid Id { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoOperacion { get; set; }
        public int Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoAutorizacion { get; set; }
        public Guid TipoAutorizacionId { get; set; }
    }
}
