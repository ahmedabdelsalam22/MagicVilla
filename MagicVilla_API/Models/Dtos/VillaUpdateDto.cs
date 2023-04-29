using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dtos
{
    public class VillaUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public String Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public String Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
