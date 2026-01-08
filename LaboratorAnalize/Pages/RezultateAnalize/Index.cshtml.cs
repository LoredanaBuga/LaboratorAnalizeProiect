using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.RezultateAnalize
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

        public IList<RezultatAnaliza> RezultatAnaliza { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.RezultatAnaliza
                .Include(r => r.TipAnaliza)
                .Include(r => r.BuletinAnalize)
                    .ThenInclude(b => b.Programare)
                .AsNoTracking()
                .AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                query = query.Where(r =>
                    r.BuletinAnalize != null &&
                    r.BuletinAnalize.Programare != null &&
                    r.BuletinAnalize.Programare.UserId == currentUserId
                );
            }

            RezultatAnaliza = await query.ToListAsync();
        }
    }
}
