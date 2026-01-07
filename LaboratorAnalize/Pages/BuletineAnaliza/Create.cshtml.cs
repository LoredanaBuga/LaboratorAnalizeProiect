using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorAnalize.Pages.BuletineAnaliza
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
            ViewData["ProgramareID"] = new SelectList(
    _context.Programare
        .Include(p => p.Pacient)
        .OrderByDescending(p => p.DataProgramare)
        .Select(p => new
        {
            p.ID,
            Text = (p.Pacient != null ? p.Pacient.NumeComplet : "Fara pacient") +
                   " - " + p.DataProgramare.ToString("dd.MM.yyyy") +
                   " (" + p.Status + ")"
        })
        .ToList(),
    "ID",
    "Text"
);

            return Page();
        }

        [BindProperty]
        public BuletinAnalize BuletinAnalize { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProgramareID"] = new SelectList(
    _context.Programare
        .Include(p => p.Pacient)
        .OrderByDescending(p => p.DataProgramare)
        .Select(p => new
        {
            p.ID,
            Text = (p.Pacient != null ? p.Pacient.NumeComplet : "Fara pacient") +
                   " - " + p.DataProgramare.ToString("dd.MM.yyyy") +
                   " (" + p.Status + ")"
        })
        .ToList(),
    "ID",
    "Text"
);

                return Page();
            }

            _context.BuletinAnalize.Add(BuletinAnalize);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
