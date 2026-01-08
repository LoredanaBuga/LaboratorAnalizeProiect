using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.PacheteAnalize
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public DeleteModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PachetAnalize PachetAnalize { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pachetanalize = await _context.PachetAnalize.FirstOrDefaultAsync(m => m.ID == id);

            if (pachetanalize == null)
            {
                return NotFound();
            }
            else
            {
                PachetAnalize = pachetanalize;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pachetanalize = await _context.PachetAnalize.FindAsync(id);
            if (pachetanalize != null)
            {
                PachetAnalize = pachetanalize;
                _context.PachetAnalize.Remove(PachetAnalize);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
