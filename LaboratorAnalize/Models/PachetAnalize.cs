using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboratorAnalize.Models
{
    public class PachetAnalize
    {
        public int ID { get; set; }

        [Display(Name = "Denumire pachet")]
        public string Denumire { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        [Display(Name = "Pret")]
        public decimal Pret { get; set; }

        public ICollection<Programare>? Programari { get; set; }
    }
}
