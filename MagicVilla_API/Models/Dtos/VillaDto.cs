using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public String Name { get; set; }
    }
}
