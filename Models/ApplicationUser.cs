using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Aromo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Полето 'Име' е задължително.")]
        [MaxLength(32, ErrorMessage = "Името не може да бъде по-дълго от 32 символа.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето 'Фамилия' е задължително.")]
        [MaxLength(32, ErrorMessage = "Фамилията не може да бъде по-дълго от 32 символа.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}