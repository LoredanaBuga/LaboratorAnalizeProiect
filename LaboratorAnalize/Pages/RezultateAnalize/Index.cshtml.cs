using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.RezultateAnalize
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public IList<RezultatAnaliza> RezultatAnaliza { get;set; } = default!;

        public async Task OnGetAsync()
        {
            RezultatAnaliza = await _context.RezultatAnaliza
                .Include(r => r.BuletinAnalize)
                .Include(r => r.TipAnaliza).ToListAsync();
        }
    }
}
