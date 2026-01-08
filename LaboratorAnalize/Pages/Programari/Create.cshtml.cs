using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Pages.Programari
{
    public class CreateModel : ProgramareTipAnalizePageModel
    {
        private readonly LaboratorAnalizeContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(LaboratorAnalizeContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");

            var programare = new Programare();
            programare.ProgramareTipAnalize = new List<ProgramareTipAnaliza>();

            PopulateAssignedTipAnalizaData(_context, programare);

            return Page();
        }

        [BindProperty]
        public Programare Programare { get; set; } = new Programare();

        public async Task<IActionResult> OnPostAsync(string[] selectedTipuriAnalize)
        {
            if (selectedTipuriAnalize != null)
            {
                Programare.ProgramareTipAnalize = new List<ProgramareTipAnaliza>();

                foreach (var a in selectedTipuriAnalize)
                {
                    Programare.ProgramareTipAnalize.Add(new ProgramareTipAnaliza
                    {
                        TipAnalizaID = int.Parse(a)
                    });
                }
            }

            if (!ModelState.IsValid)
            {
                ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
                ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");

                PopulateAssignedTipAnalizaData(_context, Programare);
                return Page();
            }

            // LEGARE PROGRAMARE DE USERUL LOGAT
            Programare.UserId = _userManager.GetUserId(User);

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
