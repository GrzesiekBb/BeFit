using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BeFit.Models
{
    public class ExConn
    {
        [Key]
        public int ConnId { get; set; }

        [ForeignKey(nameof(ExType))]
        [Display(Name = "Typ ćwiczenia")]
        public int TypeId { get; set; }
        public ExType ExType { get; set; }

        [ForeignKey(nameof(SessionInfo))]
        [Display(Name = "Sesja treningowa")]
        public int SessionId { get; set; }
        public SessionInfo SessionInfo { get; set; }
    }
}

