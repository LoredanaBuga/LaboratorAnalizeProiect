using System.Collections.Generic;

namespace LaboratorAnalize.Models.ViewModels
{
    public class BuletinAnalizeIndexData
    {
        public IEnumerable<BuletinAnalize> Buletine { get; set; } = new List<BuletinAnalize>();
        public IEnumerable<RezultatAnaliza> Rezultate { get; set; } = new List<RezultatAnaliza>();

    }
}
