using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class RezultatAnaliza
    {
        public int ID { get; set; }

        [Required]
        public int BuletinAnalizeID { get; set; }
        public BuletinAnalize? BuletinAnalize { get; set; }

        [Required]
        public int TipAnalizaID { get; set; }
        public TipAnaliza? TipAnaliza { get; set; }

        [Required(ErrorMessage = "Valoarea este obligatorie.")]
        [Range(0, 100000, ErrorMessage = "Valoarea trebuie sa fie pozitiva.")]
        [Display(Name = "Valoare rezultat")]
        public decimal Valoare { get; set; }

        [Required(ErrorMessage = "Unitatea este obligatorie.")]
        [StringLength(15)]
        [Display(Name = "Unitate de masura")]
        public string Unitate { get; set; } = "mg/dL";

        [Required(ErrorMessage = "Intervalul de referinta este obligatoriu.")]
        [StringLength(40)]
        [Display(Name = "Interval referinta")]
        public string IntervalReferinta { get; set; } = string.Empty;

        [Display(Name = "Este in limite?")]
        public bool InLimite { get; set; }
    }
}
