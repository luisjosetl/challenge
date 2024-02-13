namespace AutorizacionDePagos.Api.Application.Services.ProcessorPayment
{
    public interface IProcessorPaymentService
    {
        Task<ProcessorResponseDto> PaymentProcessor(ProcessorRequestDto processorRequest);
    }
}
