using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class DetailsModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public DetailsModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public Programare Programare { get; set; } = default!;

        // Buletin + rezultate pentru programarea curenta
        public BuletinAnalize? Buletin { get; set; }
        public IList<RezultatAnaliza> Rezultate { get; set; } = new List<RezultatAnaliza>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Programare = await _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Programare == null)
                return NotFound();

            // cauta buletinul emis pentru aceasta programare
            Buletin = await _context.BuletinAnalize
                .Include(b => b.Rezultate)
                    .ThenInclude(r => r.TipAnaliza)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ProgramareID == id);

            if (Buletin != null && Buletin.Rezultate != null)
            {
                Rezultate = new List<RezultatAnaliza>(Buletin.Rezultate);
            }

            return Page();
        }
    }
}
