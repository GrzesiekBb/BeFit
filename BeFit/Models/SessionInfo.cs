using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class SessionInfo
    {
        [Key]
        [Display(Name = "ID sesji")]
        public int SessionId { get; set; }

        [Required]
        [Display(Name = "Początek sesji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "Koniec sesji")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
    }
}
