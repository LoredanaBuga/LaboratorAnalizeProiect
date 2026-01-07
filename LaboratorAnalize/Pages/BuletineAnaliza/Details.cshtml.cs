using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    public class DetailsModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public DetailsModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public BuletinAnalize BuletinAnalize { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BuletinAnalize = await _context.BuletinAnalize
                .Include(b => b.Programare)
                    .ThenInclude(p => p.Pacient)
                .Include(b => b.Programare)
                    .ThenInclude(p => p.PachetAnalize)
                .Include(b => b.Rezultate)
                    .ThenInclude(r => r.TipAnaliza)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (BuletinAnalize == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
