using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using LaboratorAnalize.Models.ViewModels;

namespace LaboratorAnalize.Pages.Pacienti
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public PacientIndexData PacientData { get; set; } = default!;
        public int PacientID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            PacientData = new PacientIndexData();

            PacientData.Pacienti = await _context.Pacient
                .Include(p => p.Programari)
                    .ThenInclude(pr => pr.PachetAnalize)
                .Include(p => p.Programari)
                    .ThenInclude(pr => pr.ProgramareTipAnalize)
                        .ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .OrderBy(p => p.Nume)
                .ThenBy(p => p.Prenume)
                .ToListAsync();

            if (id != null)
            {
                PacientID = id.Value;
                var pacient = PacientData.Pacienti.Single(p => p.ID == id.Value);
                PacientData.Programari = pacient.Programari ?? new System.Collections.Generic.List<Programare>();
            }
        }
    }
}