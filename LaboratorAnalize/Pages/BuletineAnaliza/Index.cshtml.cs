using System.Linq;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.BuletineAnaliza
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

        public BuletinAnalizeIndexData Data { get; set; } = new BuletinAnalizeIndexData();
        public int BuletinID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            var query = _context.BuletinAnalize
                .Include(b => b.Programare)
                    .ThenInclude(p => p.Pacient)
                .Include(b => b.Rezultate)
                    .ThenInclude(r => r.TipAnaliza)
                .AsNoTracking()
                .AsQueryable();

            // ✅ User vede doar buletinele lui (prin Programare.UserId)
            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                query = query.Where(b =>
                    b.Programare != null &&
                    b.Programare.UserId == currentUserId
                );
            }

            Data.Buletine = await query
                .OrderByDescending(b => b.DataEliberare)
                .ToListAsync();

            if (id != null)
            {
                BuletinID = id.Value;
                var buletin = Data.Buletine.FirstOrDefault(b => b.ID == id.Value);
                if (buletin != null)
                {
                    Data.Rezultate = buletin.Rezultate;
                }
            }
        }
    }
}
