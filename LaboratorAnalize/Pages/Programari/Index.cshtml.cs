using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public ProgramareData ProgramareD { get; set; } = default!;
        public int ProgramareID { get; set; }

        public string DataSort { get; set; } = "";
        public string PacientSort { get; set; } = "";
        public string StatusSort { get; set; } = "";
        public string CurrentFilter { get; set; } = "";

        public async Task OnGetAsync(int? id, string? sortOrder, string? searchString)
        {
            ProgramareD = new ProgramareData();

            CurrentFilter = searchString ?? "";

            DataSort = string.IsNullOrEmpty(sortOrder) ? "data_desc" : "";
            PacientSort = sortOrder == "pacient" ? "pacient_desc" : "pacient";
            StatusSort = sortOrder == "status" ? "status_desc" : "status";

            var programariQuery = _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                programariQuery = programariQuery.Where(p =>
                    (p.Pacient != null && (p.Pacient.Nume.Contains(searchString) || p.Pacient.Prenume.Contains(searchString))) ||
                    (p.PachetAnalize != null && p.PachetAnalize.Denumire.Contains(searchString)) ||
                    (p.ProgramareTipAnalize != null && p.ProgramareTipAnalize.Any(x => x.TipAnaliza.Denumire.Contains(searchString)))
                );
            }

            programariQuery = sortOrder switch
            {
                "data_desc" => programariQuery.OrderByDescending(p => p.DataProgramare),
                "pacient" => programariQuery.OrderBy(p => p.Pacient!.Nume).ThenBy(p => p.Pacient!.Prenume),
                "pacient_desc" => programariQuery.OrderByDescending(p => p.Pacient!.Nume).ThenByDescending(p => p.Pacient!.Prenume),
                "status" => programariQuery.OrderBy(p => p.Status),
                "status_desc" => programariQuery.OrderByDescending(p => p.Status),
                _ => programariQuery.OrderBy(p => p.DataProgramare)
            };

            ProgramareD.Programari = await programariQuery.ToListAsync();

            if (id != null)
            {
                ProgramareID = id.Value;
                var programare = ProgramareD.Programari.Single(i => i.ID == id.Value);

                ProgramareD.TipuriAnalize = (programare.ProgramareTipAnalize ?? new List<ProgramareTipAnaliza>())
                    .Select(s => s.TipAnaliza);
            }
        }
    }
}
