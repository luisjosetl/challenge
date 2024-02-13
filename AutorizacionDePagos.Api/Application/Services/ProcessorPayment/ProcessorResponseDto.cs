namespace AutorizacionDePagos.Api.Application.Services.ProcessorPayment
{
    public class ProcessorResponseDto
    {
        public bool Aprobado { get; set; }

        public int? Monto { get; set; }

        public decimal? MontoDenegado { get; set; }
    }
}
