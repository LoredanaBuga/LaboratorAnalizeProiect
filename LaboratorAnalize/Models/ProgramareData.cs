namespace LaboratorAnalize.Models
{
    public class ProgramareData
    {
        public IEnumerable<Programare> Programari { get; set; } = new List<Programare>();
        public IEnumerable<TipAnaliza> TipuriAnalize { get; set; } = new List<TipAnaliza>();
        public IEnumerable<ProgramareTipAnaliza> ProgramareTipAnalize { get; set; } = new List<ProgramareTipAnaliza>();
    }
}
