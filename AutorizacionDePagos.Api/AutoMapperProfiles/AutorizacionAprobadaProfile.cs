using AutoMapper;
using AutorizacionDePagos.Api.Application.ApprovedAuthorizations;
using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.AutoMapperProfiles
{
    public class AutorizacionAprobadaProfile : Profile
    {
        public AutorizacionAprobadaProfile()
        {
            CreateMap<AutorizacionAprobada, ApprovedAuthorizationsResponseDto>();
        }
    }
}
