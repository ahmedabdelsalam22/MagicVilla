using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dtos
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
        public VillaDto Villa { get; set; }
    }
}
