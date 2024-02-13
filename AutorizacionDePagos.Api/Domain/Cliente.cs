using System.ComponentModel.DataAnnotations;

namespace AutorizacionDePagos.Api.Domain
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }

        [EnumDataType(typeof(TipoClienteEnum))]
        public TipoClienteEnum TipoCliente { get; set; }
    }
}
