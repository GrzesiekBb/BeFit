using BeFit.Models;
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
        public ExType? ExType { get; set; }

        [ForeignKey(nameof(SessionInfo))]
        [Display(Name = "Sesja treningowa")]
        public int SessionId { get; set; }
        public SessionInfo? SessionInfo { get; set; }

        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Display(Name = "Powtórzeń w serii")]
        public int RepsPerSet { get; set; }

        [Display(Name = "Obciążenie (kg)")]
        public double Load { get; set; }
        [Display(Name = "Utworzone przez")]
        public string CreatedById { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatedById))]
        public AppUser? CreatedBy { get; set; }

    }
}