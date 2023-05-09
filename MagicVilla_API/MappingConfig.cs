using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            //VillaAPIController
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            //VillaNumberAPIController
            CreateMap<VillaNumber, VillaNumberDto>();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
