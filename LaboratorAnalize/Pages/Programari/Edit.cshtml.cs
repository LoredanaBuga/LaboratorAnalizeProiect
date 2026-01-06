using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class EditModel : ProgramareTipAnalizePageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public EditModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programare = await _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.TipAnaliza)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Programare == null)
            {
                return NotFound();
            }

            PopulateAssignedTipAnalizaData(_context, Programare);

            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
            ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedTipuriAnalize)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareToUpdate = await _context.Programare
                .Include(p => p.Pacient)
                .Include(p => p.PachetAnalize)
                .Include(p => p.ProgramareTipAnalize)
                    .ThenInclude(pt => pt.TipAnaliza)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (programareToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                programareToUpdate,
                "Programare",
                p => p.DataProgramare,
                p => p.OraProgramare,
                p => p.Status,
                p => p.Observatii,
                p => p.PacientID,
                p => p.PachetAnalizeID))
            {
                UpdateProgramareTipAnalize(_context, selectedTipuriAnalize, programareToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // daca nu s-a validat modelul, repopulezi checkbox-urile si dropdown-urile
            UpdateProgramareTipAnalize(_context, selectedTipuriAnalize, programareToUpdate);
            PopulateAssignedTipAnalizaData(_context, programareToUpdate);

            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
            ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");

            return Page();
        }
    }
}
