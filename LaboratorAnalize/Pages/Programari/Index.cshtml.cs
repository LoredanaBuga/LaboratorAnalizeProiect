using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.Programari
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(LaboratorAnalizeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                programariQuery = programariQuery.Where(p => p.UserId == currentUserId);
            }

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

                var programare = ProgramareD.Programari.SingleOrDefault(i => i.ID == id.Value);
                if (programare == null)
                {
                    return;
                }

                ProgramareD.TipuriAnalize = (programare.ProgramareTipAnalize ?? new List<ProgramareTipAnaliza>())
                    .Select(s => s.TipAnaliza);
            }
        }
    }
}
