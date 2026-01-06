using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class DeleteModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public DeleteModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programare = await _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Programare == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programare
                .Include(p => p.ProgramareTipAnalize)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (programare == null)
            {
                return RedirectToPage("./Index");
            }

            if (programare.ProgramareTipAnalize != null && programare.ProgramareTipAnalize.Count > 0)
            {
                _context.ProgramareTipAnaliza.RemoveRange(programare.ProgramareTipAnalize);
            }

            _context.Programare.Remove(programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
