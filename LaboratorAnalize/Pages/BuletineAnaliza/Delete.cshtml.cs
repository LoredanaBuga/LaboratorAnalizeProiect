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

namespace LaboratorAnalize.Pages.BuletineAnaliza
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
        public BuletinAnalize BuletinAnalize { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buletinanalize = await _context.BuletinAnalize.FirstOrDefaultAsync(m => m.ID == id);

            if (buletinanalize == null)
            {
                return NotFound();
            }
            else
            {
                BuletinAnalize = buletinanalize;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buletinanalize = await _context.BuletinAnalize.FindAsync(id);
            if (buletinanalize != null)
            {
                BuletinAnalize = buletinanalize;
                _context.BuletinAnalize.Remove(BuletinAnalize);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
