using AutoMapper;
using MagicVilla_web.Models.Dtos;

namespace MagicVilla_web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNumberDto, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
