using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.RezultateAnalize
{
    public class DetailsModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public DetailsModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public RezultatAnaliza RezultatAnaliza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RezultatAnaliza = await _context.RezultatAnaliza
                .Include(r => r.TipAnaliza)
                .Include(r => r.BuletinAnalize)
                    .ThenInclude(b => b.Programare)
                        .ThenInclude(p => p.Pacient)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (RezultatAnaliza == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
