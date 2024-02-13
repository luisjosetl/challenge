using AutorizacionDePagos.Api.Application.Services.ExternalClientService;

namespace AutorizacionDePagos.Api.Application.Services.ProcessorPayment
{
    public class ProcessorPaymentService : IProcessorPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientService httpClientService;

        public ProcessorPaymentService(
            IConfiguration configuration,
            IHttpClientService httpClientService)
        {
            this.configuration = configuration;
            this.httpClientService = httpClientService;
        }

        public async Task<ProcessorResponseDto> PaymentProcessor(ProcessorRequestDto processorRequest)
        {
            try
            {
                var url = configuration.GetValue<string>("ProcessorBaseUrl");

                var response = await httpClientService.PostAsync<ProcessorRequestDto, ProcessorResponseDto>(processorRequest, url);

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
