using MagicVilla_API.Models.Dtos;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>()
            {
             new VillaDto {Id = 1,Name = "Pool View"},
             new VillaDto {Id = 1,Name = "Beach View"},
            };
    }
}
