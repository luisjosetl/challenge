namespace AutorizacionDePagos.Api.Domain
{
    public class AutorizacionAprobada : BaseEntity
    {
        public string NombreCliente { get; set; }

        public string CodigoOperacion { get; set; }

        public int Monto { get; set; }

        public Guid TipoAutorizacionId { get; set; }
    }
}
