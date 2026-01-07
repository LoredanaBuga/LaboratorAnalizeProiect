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
    public class DetailsModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public DetailsModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

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
    }
}
