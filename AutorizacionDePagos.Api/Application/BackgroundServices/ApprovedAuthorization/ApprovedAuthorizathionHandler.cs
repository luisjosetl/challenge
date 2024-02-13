using AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization;
using AutorizacionDePagos.Api.Domain;
using EasyNetQ;

namespace AutorizacionDePagos.Api.Application.ApprovedAuthorization
{
    public class ApprovedAuthorizathionHandler : BackgroundService
    {
        private readonly IBus bus;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public ApprovedAuthorizathionHandler(
            IBus bus,
            IServiceProvider serviceProvider,
            IConfiguration configuration
            )
        {
            this.bus = bus;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        
                        await bus.PubSub.SubscribeAsync<AutorizacionAprobada>("ApprovedAuthorizathionHandler_subscription", async message => await ProcessMessageAsync(message));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");                        
                    }
                    

                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ApprovedAuthorizathionHandler {ex.Message}");                
                throw;
            }
            
        }

        private async Task ProcessMessageAsync(AutorizacionAprobada approvedAuthorization)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IApprovedAuthorizationService>();
                await service.SaveApprovedAuthorization(approvedAuthorization);
            }
        }
    }
}
