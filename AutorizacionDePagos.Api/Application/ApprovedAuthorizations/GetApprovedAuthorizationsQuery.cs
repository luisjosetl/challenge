using AutoMapper;
using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;
using MediatR;

namespace AutorizacionDePagos.Api.Application.ApprovedAuthorizations
{
    public class GetApprovedAuthorizationsQuery : IRequest<List<ApprovedAuthorizationsResponseDto>>
    {
    }

    public class GetApprovedAuthorizationsQueryHandler : IRequestHandler<GetApprovedAuthorizationsQuery, List<ApprovedAuthorizationsResponseDto>>
    {
        private readonly IRepository<AutorizacionAprobada> autorizacionAprobadaRepository;
        private readonly ITipoAutorizacionRepository tipoAutorizacionRepository;
        private readonly IMapper mapper;

        public GetApprovedAuthorizationsQueryHandler(
            IRepository<AutorizacionAprobada> autorizacionAprobadaRepository,
            ITipoAutorizacionRepository tipoAutorizacionRepository,
            IMapper mapper
            )
        {
            this.autorizacionAprobadaRepository = autorizacionAprobadaRepository;
            this.tipoAutorizacionRepository = tipoAutorizacionRepository;
            this.mapper = mapper;
        }

        public async Task<List<ApprovedAuthorizationsResponseDto>> Handle(GetApprovedAuthorizationsQuery request, CancellationToken cancellationToken)
        {
            var approvedAuthorizations = await autorizacionAprobadaRepository.GetAllListAsync();

            var approvedAuthorizationsMapped = mapper.Map<List<ApprovedAuthorizationsResponseDto>>(approvedAuthorizations);

            if (approvedAuthorizationsMapped.Any())
            {
                var authorizationTypes = await tipoAutorizacionRepository.GetAllList();

                foreach (var authorization in approvedAuthorizationsMapped)
                    authorization.TipoAutorizacion = authorizationTypes.FirstOrDefault(t => t.Id == authorization.TipoAutorizacionId)?.Nombre ?? "";
            }

            return approvedAuthorizationsMapped;


        }
    }
}
