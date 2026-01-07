using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class RezultatAnaliza
    {
        public int ID { get; set; }

        // Buletinul in care apare rezultatul
        public int? BuletinAnalizeID { get; set; }
        public BuletinAnalize? BuletinAnalize { get; set; }

        // Tipul analizei (Hemoleucograma, Glicemie etc.)
        public int? TipAnalizaID { get; set; }
        public TipAnaliza? TipAnaliza { get; set; }

        [Display(Name = "Valoare rezultat")]
        public string? Valoare { get; set; }

        [Display(Name = "Unitate de masura")]
        public string? Unitate { get; set; }

        [Display(Name = "Interval referinta")]
        public string? IntervalReferinta { get; set; }

        [Display(Name = "Este in limite?")]
        public bool InLimite { get; set; } = true;
    }
}
