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
    public class DeleteModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public DeleteModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RezultatAnaliza RezultatAnaliza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatanaliza = await _context.RezultatAnaliza.FirstOrDefaultAsync(m => m.ID == id);

            if (rezultatanaliza == null)
            {
                return NotFound();
            }
            else
            {
                RezultatAnaliza = rezultatanaliza;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatanaliza = await _context.RezultatAnaliza.FindAsync(id);
            if (rezultatanaliza != null)
            {
                RezultatAnaliza = rezultatanaliza;
                _context.RezultatAnaliza.Remove(RezultatAnaliza);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
