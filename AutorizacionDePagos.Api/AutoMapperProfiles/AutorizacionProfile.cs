using AutoMapper;
using AutorizacionDePagos.Api.Application.Authorization;
using AutorizacionDePagos.Api.Domain;

namespace AutorizacionDePagos.Api.AutoMapperProfiles
{
    public class AutorizacionProfile : Profile
    {
        public AutorizacionProfile()
        {
            CreateMap<Autorizacion, AuthorizationResponseDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre))
                .ForMember(dest => dest.TipoAutorizacion, opt => opt.MapFrom(src => src.TipoAutorizacion.Nombre));
        }
    }
}
