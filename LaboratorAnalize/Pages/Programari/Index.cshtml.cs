using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public ProgramareData ProgramareD { get; set; } = default!;
        public int ProgramareID { get; set; }

        public async Task OnGetAsync(int ? id)
        {
            ProgramareD = new ProgramareData();

            ProgramareD.Programari = await _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize).ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .OrderBy(p => p.DataProgramare)
                .ToListAsync();

            if (id != null)
            {
                ProgramareID = id.Value;
                var programare = ProgramareD.Programari.Where(i => i.ID == id.Value).Single();
                ProgramareD.TipuriAnalize = (programare.ProgramareTipAnalize ?? new List<ProgramareTipAnaliza>()).Select(s => s.TipAnaliza);
            }
        }
    }
}
