using AutoMapper;
using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;
using MediatR;

namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class GetReverseAuthorizationsQuery : IRequest<List<AuthorizationResponseDto>>
    {
    }

    public class GetReverseAuthorizationsQueryHandler : IRequestHandler<GetReverseAuthorizationsQuery, List<AuthorizationResponseDto>>
    {
        private readonly IAutorizacionRepository autorizacionRepository;
        private readonly IMapper mapper;

        public GetReverseAuthorizationsQueryHandler(
            IAutorizacionRepository autorizacionRepository, 
            IMapper mapper)
        {
            this.autorizacionRepository = autorizacionRepository;
            this.mapper = mapper;
        }

        public async Task<List<AuthorizationResponseDto>> Handle(GetReverseAuthorizationsQuery request, CancellationToken cancellationToken)
        {
            var authorizations = await autorizacionRepository.GetAllAutorizaciones();

            authorizations = authorizations
                .Where(a => a.TipoAutorizacion.Nombre == TipoAutorizacionEnum.REVERSA.ToString())
                .ToList();

            var athorizationsMapped = mapper.Map<List<AuthorizationResponseDto>>(authorizations);

            return athorizationsMapped;
        }
    }
}
