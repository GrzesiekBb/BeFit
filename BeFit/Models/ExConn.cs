using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExConn
    {
        [Key]
        [Display(Name = "ID wpisu")]
        public int ConnId { get; set; }

        [Display(Name = "Typ ćwiczenia")]
        public int TypeId { get; set; }
        public ExType ExType { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int SessionId { get; set; }
        public SessionInfo SessionInfo { get; set; }
    }
}
