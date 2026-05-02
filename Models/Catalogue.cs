using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aromo.Models
{
    public class Catalogue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [StringLength(32,  ErrorMessage = "Името не може да бъде по дълго от 32 символа")]
        public required string Name { get; set; }

        public ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();
    }
}
