using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class Pacient
    {
        public int ID { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Numele trebuie sa aiba intre 2 si 30 de caractere.")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s\-]*$", ErrorMessage = "Numele trebuie sa inceapa cu litera mare.")]
        public string Nume { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Prenumele trebuie sa aiba intre 2 si 30 de caractere.")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s\-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu litera mare.")]
        public string Prenume { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNP-ul este obligatoriu.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP-ul trebuie sa aiba exact 13 cifre.")]
        public string CNP { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email invalid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefonul este obligatoriu.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Telefonul trebuie sa inceapa cu 0 si sa aiba 10 cifre.")]
        public string Telefon { get; set; } = string.Empty;

        public ICollection<Programare>? Programari { get; set; }

        public string NumeComplet => Nume + " " + Prenume;
    }
}
