using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.Programari
{
    public class DeleteModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(LaboratorAnalizeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                if (Programare.UserId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
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

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                if (programare.UserId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
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
