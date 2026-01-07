using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.RezultateAnalize
{
    public class CreateModel : PageModel
    {
        private readonly LaboratorAnalizeContext _context;

        public CreateModel(LaboratorAnalizeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RezultatAnaliza RezultatAnaliza { get; set; } = new RezultatAnaliza();

        public IActionResult OnGet()
        {
            PopulateDropDowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns();
                return Page();
            }

            _context.RezultatAnaliza.Add(RezultatAnaliza);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void PopulateDropDowns(int? selectedBuletin = null, int? selectedTip = null)
        {
            var buletine = _context.BuletinAnalize
                .Include(b => b.Programare)
                    .ThenInclude(p => p.Pacient)
                .OrderByDescending(b => b.DataEliberare)
                .Select(b => new
                {
                    b.ID,
                    Text = "Buletin #" + b.ID + " - " +
                           (b.Programare != null && b.Programare.Pacient != null ? b.Programare.Pacient.NumeComplet : "Fara pacient")
                           + " - " + b.DataEliberare.ToString("dd.MM.yyyy")
                })
                .ToList();

            ViewData["BuletinAnalizeID"] = new SelectList(buletine, "ID", "Text", selectedBuletin);

            var tipuri = _context.TipAnaliza
                .OrderBy(t => t.Denumire)
                .Select(t => new
                {
                    t.ID,
                    Text = t.Denumire + " (" + t.TipProba + ")"
                })
                .ToList();

            ViewData["TipAnalizaID"] = new SelectList(tipuri, "ID", "Text", selectedTip);
        }
    }
}
