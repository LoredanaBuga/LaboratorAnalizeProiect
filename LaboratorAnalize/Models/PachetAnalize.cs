using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboratorAnalize.Models
{
    public class PachetAnalize
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Denumirea pachetului este obligatorie.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Denumirea trebuie sa aiba intre 3 si 80 caractere.")]
        [Display(Name = "Denumire pachet")]
        public string Denumire { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        [Range(1, 5000, ErrorMessage = "Pretul trebuie sa fie intre 1 si 5000.")]
        [Display(Name = "Pret")]
        public decimal Pret { get; set; }

        public ICollection<Programare>? Programari { get; set; }
    }
}
