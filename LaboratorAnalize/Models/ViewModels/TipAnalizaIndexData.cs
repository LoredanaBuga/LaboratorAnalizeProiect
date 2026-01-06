namespace LaboratorAnalize.Models.ViewModels
{
    public class TipAnalizaIndexData
    {
        public IEnumerable<TipAnaliza> TipuriAnalize { get; set; } = new List<TipAnaliza>();
        public IEnumerable<Programare> Programari { get; set; } = new List<Programare>();
    }
}
