using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    public class IndexModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public IndexModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public IList<BuletinAnalize> BuletinAnalize { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BuletinAnalize = await _context.BuletinAnalize
                .Include(b => b.Programare).ToListAsync();
        }
    }
}
