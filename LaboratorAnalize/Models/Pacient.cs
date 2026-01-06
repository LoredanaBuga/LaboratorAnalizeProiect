using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class Pacient
    {
        public int ID { get; set; }

        [Display(Name = "Nume")]
        public string Nume { get; set; } = string.Empty;

        [Display(Name = "Prenume")]
        public string Prenume { get; set; } = string.Empty;

        [Display(Name = "CNP")]
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNP-ul trebuie sa aiba 13 cifre")]
        public string CNP { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }

        public ICollection<Programare>? Programari { get; set; }

        [Display(Name = "Nume complet")]
        public string NumeComplet => Nume + " " + Prenume;
    }
}
