using AutoMapper;
using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Interfaces;
using MediatR;

namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class GetAllAuthorizationsQuery : IRequest<List<AuthorizationResponseDto>>
    {
    }

    public class GetAllAuthorizationsQueryHandler : IRequestHandler<GetAllAuthorizationsQuery, List<AuthorizationResponseDto>>
    {
        private readonly IAutorizacionRepository autorizacionRepository;
        private readonly IMapper mapper;

        public GetAllAuthorizationsQueryHandler(IAutorizacionRepository autorizacionRepository, IMapper mapper)
        {
            this.autorizacionRepository = autorizacionRepository;
            this.mapper = mapper;
        }

        public async Task<List<AuthorizationResponseDto>> Handle(GetAllAuthorizationsQuery request, CancellationToken cancellationToken)
        {
            var authorizations = await autorizacionRepository.GetAllAutorizaciones();

            var athorizationsMapped = mapper.Map<List<AuthorizationResponseDto>>(authorizations);

            return athorizationsMapped;
        }
    }
}
