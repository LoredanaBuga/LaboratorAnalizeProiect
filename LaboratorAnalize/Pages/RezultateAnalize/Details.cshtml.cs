using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.RezultateAnalize
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

            // User poate vedea doar rezultatele lui
            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                var ownerId = RezultatAnaliza.BuletinAnalize?.Programare?.UserId;

                if (ownerId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
            }

            return Page();
        }
    }
}
