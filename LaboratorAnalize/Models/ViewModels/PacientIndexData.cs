using System.Collections.Generic;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Models.ViewModels
{
    public class PacientIndexData
    {
        public IEnumerable<Pacient> Pacienti { get; set; } = new List<Pacient>();
        public IEnumerable<Programare> Programari { get; set; } = new List<Programare>();
    }
}
