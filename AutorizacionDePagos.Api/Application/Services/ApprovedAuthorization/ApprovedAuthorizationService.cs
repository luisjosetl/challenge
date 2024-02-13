using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;
using EasyNetQ;

namespace AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization
{
    public class ApprovedAuthorizationService : IApprovedAuthorizationService
    {
        private readonly IBus bus;
        private readonly IRepository<AutorizacionAprobada> autorizacionAprobadaRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApprovedAuthorizationService(
            IBus bus,
            IRepository<AutorizacionAprobada> autorizacionAprobadaRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.bus = bus;
            this.autorizacionAprobadaRepository = autorizacionAprobadaRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task ApprovedAuthorizationPublish(Autorizacion authorization)
        {
            var approvedAuthorization = new AutorizacionAprobada()
            {
                NombreCliente = authorization.NombreCliente,
                CodigoOperacion = authorization.CodigoOperacion,
                Monto = authorization.Monto ?? 0,
                TipoAutorizacionId = authorization.TipoAutorizacionId
            };

            await bus.PubSub.PublishAsync(approvedAuthorization);
        }

        public async Task SaveApprovedAuthorization(AutorizacionAprobada approvedAuthorization)
        {
            try
            {
                await autorizacionAprobadaRepository.AddAsync(approvedAuthorization);
                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }


    }
}
