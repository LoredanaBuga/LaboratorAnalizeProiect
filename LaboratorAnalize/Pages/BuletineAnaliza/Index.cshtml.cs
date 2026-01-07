using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models.ViewModels;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public BuletinAnalizeIndexData Data { get; set; } = new BuletinAnalizeIndexData();
        public int BuletinID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Data.Buletine = await _context.BuletinAnalize
        .Include(b => b.Programare)
            .ThenInclude(p => p.Pacient)
        .Include(b => b.Rezultate) // Modificat aici (fără "Analize")
            .ThenInclude(r => r.TipAnaliza)
        .AsNoTracking()
        .OrderByDescending(b => b.DataEliberare) // Acum ar trebui să meargă dacă ai using-ul pus
        .ToListAsync();

            if (id != null)
            {
                BuletinID = id.Value;
                var buletin = Data.Buletine.First(b => b.ID == id.Value);
                Data.Rezultate = buletin.Rezultate; // Modificat aici
            }
        }
    }
}
