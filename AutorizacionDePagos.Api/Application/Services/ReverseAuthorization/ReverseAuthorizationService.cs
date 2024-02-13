using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;

namespace AutorizacionDePagos.Api.Application.Services.ReverseAuthorization
{
    public class ReverseAuthorizationService : IReverseAuthorizationService
    {
        private readonly IAutorizacionRepository autorizacionRepository;
        private readonly IRepository<Autorizacion> autorizacionGenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEstadoRepository estadoRepository;
        private readonly ITipoAutorizacionRepository tipoAutorizacionRepository;

        private readonly int MINUTES_LIMIT = 5;

        public ReverseAuthorizationService(
            IAutorizacionRepository autorizacionRepository,
            IRepository<Autorizacion> autorizacionGenericRepository,
            IUnitOfWork unitOfWork,
            IEstadoRepository estadoRepository,
            ITipoAutorizacionRepository tipoAutorizacionRepository
            )
        {
            this.autorizacionRepository = autorizacionRepository;
            this.autorizacionGenericRepository = autorizacionGenericRepository;
            this.unitOfWork = unitOfWork;
            this.estadoRepository = estadoRepository;
            this.tipoAutorizacionRepository = tipoAutorizacionRepository;
        }

        public async Task ChangeStateToReverseAndSave()
        {
            var authorizations = await autorizacionRepository.GetAutorizacionesForReversing(MINUTES_LIMIT);
            var newAuthorizations = new List<Autorizacion>();

            if (authorizations.Any())
            {
                var noConfirmedState = await estadoRepository.GetStateByName(EstadoEnum.NO_CONFIRMADO);
                var deniedState = await estadoRepository.GetStateByName(EstadoEnum.DENEGADO);
                var authorizationReverseType = await tipoAutorizacionRepository.GetAuthorizationTypeByName(TipoAutorizacionEnum.REVERSA);

                foreach (var authorization in authorizations)
                {
                    authorization.EstadoAutorizacionId = deniedState.Id;
                    autorizacionGenericRepository.UpdateAsync(authorization);

                    var newAuthorization = new Autorizacion()
                    {
                       NombreCliente = authorization.NombreCliente,
                       CodigoOperacion = authorization.CodigoOperacion,
                       Monto = authorization.Monto,
                       TipoAutorizacionId = authorizationReverseType.Id,
                       EstadoAutorizacionId = noConfirmedState.Id
                    };

                    newAuthorizations.Add(newAuthorization);
                }

                await autorizacionGenericRepository.AddRangeAsync(newAuthorizations);
                await unitOfWork.CommitAsync();
            }
        }

    }
}
