using AutoMapper;
using AutorizacionDePagos.Api.Application.Services.Authorization;
using MediatR;

namespace AutorizacionDePagos.Api.Application.Authorization
{
    public class CreateAuthorizationCommand : IRequest<AuthorizationResponseDto>
    {
        public CreateAuthorizationRequest AuthorizationRequest { get; set; }
    }

    public class CreateAuthorizationCommandHandler : IRequestHandler<CreateAuthorizationCommand, AuthorizationResponseDto>
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IMapper mapper;

        public CreateAuthorizationCommandHandler(
            IAuthorizationService authorizationService,
            IMapper mapper
            )
        {
            this.authorizationService = authorizationService;
            this.mapper = mapper;
        }

        public async Task<AuthorizationResponseDto> Handle(CreateAuthorizationCommand request, CancellationToken cancellationToken)
        {

            var authorization = await authorizationService.Create(request.AuthorizationRequest);

            var authorizationMapped = mapper.Map<AuthorizationResponseDto>(authorization);

            return authorizationMapped;
        }
    }
}
