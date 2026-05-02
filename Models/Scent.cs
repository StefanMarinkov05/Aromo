using System.ComponentModel.DataAnnotations;

namespace Aromo.Models
{
    public class Scent
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името не може да бъде празно")]
        [StringLength(100, ErrorMessage = "Името не може да бъде по дълго от 32 символа")]
        public string Name { get; set; }
    }
}
