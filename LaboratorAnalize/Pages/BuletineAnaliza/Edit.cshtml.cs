using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    public class EditModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public EditModel(LaboratorAnalizeContext context)
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

            BuletinAnalize = await _context.BuletinAnalize
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (BuletinAnalize == null)
            {
                return NotFound();
            }

            PopulateProgramariDropDown(BuletinAnalize.ProgramareID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // IMPORTANT: repopulezi dropdown-ul
                PopulateProgramariDropDown(BuletinAnalize.ProgramareID);
                return Page();
            }

            _context.Attach(BuletinAnalize).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuletinAnalizeExists(BuletinAnalize.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private void PopulateProgramariDropDown(int? selectedProgramareId = null)
        {
            var programari = _context.Programare
                .Include(p => p.Pacient)
                .OrderByDescending(p => p.DataProgramare)
                .Select(p => new
                {
                    p.ID,
                    Text = (p.Pacient != null ? p.Pacient.NumeComplet : "Fara pacient")
                           + " - " + p.DataProgramare.ToString("dd.MM.yyyy")
                           + " (" + p.Status + ")"
                })
                .ToList();

            ViewData["ProgramareID"] = new SelectList(programari, "ID", "Text", selectedProgramareId);
        }

        private bool BuletinAnalizeExists(int id)
        {
            return _context.BuletinAnalize.Any(e => e.ID == id);
        }
    }
}
