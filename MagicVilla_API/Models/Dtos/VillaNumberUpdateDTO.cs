using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dtos
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
    }
}
