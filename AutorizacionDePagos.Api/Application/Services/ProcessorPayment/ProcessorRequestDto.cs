namespace AutorizacionDePagos.Api.Application.Services.ProcessorPayment
{
    public class ProcessorRequestDto
    {
        public string NombreCliente { get; set; }

        public string Monto { get; set; }
    }
}
