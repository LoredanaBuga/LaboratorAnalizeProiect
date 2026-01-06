using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.TipuriAnalize
{
    public class DeleteModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public DeleteModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TipAnaliza TipAnaliza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipanaliza = await _context.TipAnaliza.FirstOrDefaultAsync(m => m.ID == id);

            if (tipanaliza == null)
            {
                return NotFound();
            }
            else
            {
                TipAnaliza = tipanaliza;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipanaliza = await _context.TipAnaliza.FindAsync(id);
            if (tipanaliza != null)
            {
                TipAnaliza = tipanaliza;
                _context.TipAnaliza.Remove(TipAnaliza);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
