using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.BuletineAnaliza
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public CreateModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BuletinAnalize BuletinAnalize { get; set; } = new BuletinAnalize();

        public IActionResult OnGet(int? programareId)
        {
            if (programareId != null)
            {
                BuletinAnalize.ProgramareID = programareId;
            }

            PopulateProgramariDropDown(BuletinAnalize.ProgramareID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateProgramariDropDown(BuletinAnalize.ProgramareID);
                return Page();
            }

            _context.BuletinAnalize.Add(BuletinAnalize);
            await _context.SaveChangesAsync();

            if (BuletinAnalize.ProgramareID != null)
            {
                return RedirectToPage("/Programari/Details", new { id = BuletinAnalize.ProgramareID });
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
    }
}
