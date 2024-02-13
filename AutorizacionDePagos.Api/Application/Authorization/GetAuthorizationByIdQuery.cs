using AutoMapper;
using AutorizacionDePagos.Api.Interfaces;
using MediatR;

namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class GetAuthorizationByIdQuery : IRequest<AuthorizationResponseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetAuthorizationByIdQueryHandler : IRequestHandler<GetAuthorizationByIdQuery, AuthorizationResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IAutorizacionRepository autorizacionRepository;

        public GetAuthorizationByIdQueryHandler(IMapper mapper, IAutorizacionRepository autorizacionRepository)
        {
            this.mapper = mapper;
            this.autorizacionRepository = autorizacionRepository;
        }

        public async Task<AuthorizationResponseDto> Handle(GetAuthorizationByIdQuery request, CancellationToken cancellationToken)
        {
            var authorization = await autorizacionRepository.GetAutorizacionesById(request.Id);

            var authorizationMapped = mapper.Map<AuthorizationResponseDto>(authorization);

            return authorizationMapped;
        }
    }
}
