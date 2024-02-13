using AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization;
using AutorizacionDePagos.Api.Application.Services.ReverseAuthorization;
using Microsoft.Extensions.DependencyInjection;

namespace AutorizacionDePagos.Api.Application.BackgroundServices.ReverseAuthorization
{
    public class ReverseAuthorizationHandler : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public ReverseAuthorizationHandler(
            IServiceProvider serviceProvider
            )
        {
            this.serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteReverseAuthorization();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ExecuteReverseAuthorization()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IReverseAuthorizationService>();
                await service.ChangeStateToReverseAndSave();
            }
        }
    }
}
