using System.Collections.Generic;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.Programari
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

        public Programare Programare { get; set; } = default!;

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

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                if (Programare.UserId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
            }

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
