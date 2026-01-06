using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class CreateModel : PageModel
    {
        private readonly LaboratorAnalize.Data.LaboratorAnalizeContext _context;

        public CreateModel(LaboratorAnalize.Data.LaboratorAnalizeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
        ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
            return Page();
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
                ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
                return Page();
            }

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
