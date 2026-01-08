using System;
using System.Linq;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaboratorAnalize.Pages.Programari
{
    public class EditModel : ProgramareTipAnalizePageModel
    {
        private readonly LaboratorAnalizeContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(LaboratorAnalizeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // ✅ VERIFICARE: daca nu e Admin, trebuie sa fie programarea lui
            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                if (Programare.UserId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
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

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = _userManager.GetUserId(User);
                if (programareToUpdate.UserId != currentUserId)
                {
                    return Forbid(); // sau NotFound();
                }
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

            UpdateProgramareTipAnalize(_context, selectedTipuriAnalize, programareToUpdate);
            PopulateAssignedTipAnalizaData(_context, programareToUpdate);

            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
            ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");

            return Page();
        }
    }
}
