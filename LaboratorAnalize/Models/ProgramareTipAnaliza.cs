namespace LaboratorAnalize.Models
{
    public class ProgramareTipAnaliza
    {
        public int ID { get; set; }

        public int ProgramareID { get; set; }
        public Programare Programare { get; set; } = default!;

        public int TipAnalizaID { get; set; }
        public TipAnaliza TipAnaliza { get; set; } = default!;
    }
}
