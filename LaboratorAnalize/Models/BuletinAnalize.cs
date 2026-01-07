using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class BuletinAnalize
    {
        public int ID { get; set; }

        // Legatura cu programarea (pentru care se emit rezultatele)
        public int? ProgramareID { get; set; }
        public Programare? Programare { get; set; }

        [Display(Name = "Data eliberare")]
        [DataType(DataType.Date)]
        public DateTime DataEliberare { get; set; } = DateTime.Today;

        [Display(Name = "Observatii buletin")]
        public string? Observatii { get; set; }

        public ICollection<RezultatAnaliza>? Rezultate { get; set; }
    }
}
