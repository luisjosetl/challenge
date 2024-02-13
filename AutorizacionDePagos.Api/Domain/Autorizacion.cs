namespace AutorizacionDePagos.Api.Domain
{
    public class Autorizacion : BaseEntity
    {
        public string NombreCliente { get; set; }

        public string CodigoOperacion { get; set; }

        public int? Monto { get; set; }
        
        public decimal? MontoDenegado { get; set; }

        public Guid TipoAutorizacionId { get; set; }
        public TipoAutorizacion TipoAutorizacion { get; set; }

        public Guid EstadoAutorizacionId { get; set; }
        public Estado Estado { get; set; }
    }
}
