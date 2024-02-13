using System.Text.Json.Serialization;

namespace AutorizacionDePagos.Api.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoAutorizacionEnum
    {
        COBRO,
        DEVOLUCION,
        REVERSA
    }
}
