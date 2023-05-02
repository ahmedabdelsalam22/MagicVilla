using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dtos
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
    }
}
