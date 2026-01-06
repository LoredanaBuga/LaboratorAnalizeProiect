using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAnalize.Models
{
    public class Programare
    {
        public int ID { get; set; }

        [Display(Name = "Data programare")]
        [DataType(DataType.Date)]
        public DateTime DataProgramare { get; set; }

        [Display(Name = "Ora programare")]
        [DataType(DataType.Time)]
        public TimeSpan? OraProgramare { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "In asteptare";

        [Display(Name = "Observatii")]
        public string? Observatii { get; set; }

        // FK Pacient
        [Display(Name = "Pacient")]
        public int? PacientID { get; set; }
        public Pacient? Pacient { get; set; }

        // FK PachetAnalize
        [Display(Name = "Pachet analize")]
        public int? PachetAnalizeID { get; set; }
        public PachetAnalize? PachetAnalize { get; set; }

        public ICollection<ProgramareTipAnaliza>? ProgramareTipAnalize { get; set; }
    }
}
