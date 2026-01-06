
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class TipAnaliza
    {
        public int ID { get; set; }

        [Display(Name = "Denumire analiza")]
        public string Denumire { get; set; } = string.Empty;

        [Display(Name = "Tip proba")]
        public string TipProba { get; set; } = string.Empty;
        // ex: Sange, Urina, Scaun, Saliva

        [Display(Name = "Necesita nemancat?")]
        public bool NecesitaNemancat { get; set; }

        public ICollection<ProgramareTipAnaliza>? ProgramareTipAnalize { get; set; }
    }

}
