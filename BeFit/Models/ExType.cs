using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Nazwa ćwiczenia", Description = "Np. Przysiad, Wyciskanie leżąc")]
        public string Name { get; set; } = string.Empty;
    }
}
