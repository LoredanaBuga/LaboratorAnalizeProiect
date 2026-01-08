using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    public class DetailsModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(LaboratorAnalizeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // ✅ VERIFICARE: User vede doar buletinele lui
            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                var ownerId = BuletinAnalize.Programare?.UserId;

                if (ownerId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
            }

            return Page();
        }
    }
}
