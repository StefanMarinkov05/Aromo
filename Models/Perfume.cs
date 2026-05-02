using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aromo.Models
{
    [NoScentDuplications]
    public class Perfume
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(100, ErrorMessage = "Името не може да бъде по-дълго от 100 символа")]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(100, ErrorMessage = "Името не може да бъде по-дълго от 100 символа")]
        [Display(Name = "Име на оригинала")]
        public string Original { get; set; } = null!;

        [Required(ErrorMessage = "Марката е задължителна")]
        [Display(Name = "Марка")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Сезонът е задължителен")]
        [Display(Name = "Сезон")]
        public int SeasonId { get; set; }

        [Required(ErrorMessage = "Полът е задължителен")]
        [Display(Name = "Пол")]
        public int GenderId { get; set; }

        public Brand Brand { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Gender Gender { get; set; } = null!;

        [Display(Name = "Базови нотки")]
        public ICollection<Scent> BaseScents { get; set; } = new HashSet<Scent>();

        [Display(Name = "Сърцеви нотки")]
        public ICollection<Scent> HeartScents { get; set; } = new HashSet<Scent>();

        [Display(Name = "Връхни нотки")]
        public ICollection<Scent> TopScents { get; set; } = new HashSet<Scent>();

        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(0.01, 10000, ErrorMessage = "Цената трябва да бъде между 0.01 и 10 000")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Цена за най малко количество")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Обемът е задължителна")]
        [Range(1, 1000, ErrorMessage = "Обема трябва да бъде между 1 и 1000mL")]
        [Display(Name = "Обем (мл)")]
        public int Volume { get; set; }

        [Display(Name = "Каталози")]
        public ICollection<Catalogue> Catalogues { get; set; } = new HashSet<Catalogue>();
    }

    public class NoScentDuplications : ValidationAttribute
    {
        public NoScentDuplications()
        {
            ErrorMessage = "Нотките не могат да се повтарят между различните категории.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var perfume = value as Perfume;
            if (perfume == null)
                return ValidationResult.Success;

            var baseIds = perfume.BaseScents.Select(s => s.Id);
            var heartIds = perfume.HeartScents.Select(s => s.Id);
            var topIds = perfume.TopScents.Select(s => s.Id);

            if (baseIds.Intersect(heartIds).Any()
             || baseIds.Intersect(topIds).Any()
             || heartIds.Intersect(topIds).Any())
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}