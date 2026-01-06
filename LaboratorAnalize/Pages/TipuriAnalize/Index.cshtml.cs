using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using LaboratorAnalize.Models.ViewModels;

namespace LaboratorAnalize.Pages.TipuriAnalize
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public TipAnalizaIndexData TipAnalizaData { get; set; } = default!;
        public int TipAnalizaID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            TipAnalizaData = new TipAnalizaIndexData();

            TipAnalizaData.TipuriAnalize = await _context.TipAnaliza
                .Include(t => t.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.Programare)
                        .ThenInclude(p => p.Pacient)
                .AsNoTracking()
                .OrderBy(t => t.Denumire)
                .ToListAsync();

            if (id != null)
            {
                TipAnalizaID = id.Value;

                var tip = TipAnalizaData.TipuriAnalize.SingleOrDefault(t => t.ID == id.Value);
                if (tip != null)
                {
                    TipAnalizaData.Programari = (tip.ProgramareTipAnalize ?? new List<ProgramareTipAnaliza>())
                        .Select(x => x.Programare)
                        .Distinct()
                        .ToList();
                }
            }
        }
    }
}
