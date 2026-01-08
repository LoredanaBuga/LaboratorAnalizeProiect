using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaboratorAnalize.Data;
using LaboratorAnalize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // folosit doar pentru User (pacientul completeaza datele lui)
        [BindProperty]
        public Pacient PacientInput { get; set; } = new Pacient();

        [BindProperty]
        public Programare Programare { get; set; } = new Programare();

        public IActionResult OnGet()
        {
            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");

            // Admin vede toti pacientii in dropdown
            if (User.IsInRole("Admin"))
            {
                ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
            }
            else
            {
                // User: precompletam daca exista deja pacientul lui
                var uid = _userManager.GetUserId(User);
                var pacient = _context.Pacient.FirstOrDefault(p => p.UserId == uid);
                if (pacient != null)
                {
                    PacientInput = new Pacient
                    {
                        Nume = pacient.Nume,
                        Prenume = pacient.Prenume,
                        CNP = pacient.CNP,
                        Email = pacient.Email,
                        Telefon = pacient.Telefon
                    };
                }
            }

            var programare = new Programare
            {
                ProgramareTipAnalize = new List<ProgramareTipAnaliza>()
            };

            PopulateAssignedTipAnalizaData(_context, programare);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedTipuriAnalize)
        {
            // setam analizele selectate
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


            Programare.UserId = _userManager.GetUserId(User);

            if (!User.IsInRole("Admin"))
            {
                var uid = Programare.UserId;

                // Validare date pacient 
                PacientInput.Programari = null;

                if (!TryValidateModel(PacientInput, nameof(PacientInput)))
                {
                    ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");
                    PopulateAssignedTipAnalizaData(_context, Programare);
                    return Page();
                }

                var pacient = _context.Pacient.FirstOrDefault(p => p.UserId == uid);

                if (pacient == null)
                {
                    pacient = new Pacient
                    {
                        UserId = uid,
                        Nume = PacientInput.Nume,
                        Prenume = PacientInput.Prenume,
                        CNP = PacientInput.CNP,
                        Email = PacientInput.Email,
                        Telefon = PacientInput.Telefon
                    };
                    _context.Pacient.Add(pacient);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // optional: actualizam datele daca userul le modifica
                    pacient.Nume = PacientInput.Nume;
                    pacient.Prenume = PacientInput.Prenume;
                    pacient.CNP = PacientInput.CNP;
                    pacient.Email = PacientInput.Email;
                    pacient.Telefon = PacientInput.Telefon;
                    await _context.SaveChangesAsync();
                }

                Programare.PacientID = pacient.ID;
            }

            // daca modelul Programare nu e valid
            if (!ModelState.IsValid)
            {
                ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");

                if (User.IsInRole("Admin"))
                    ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");

                PopulateAssignedTipAnalizaData(_context, Programare);
                return Page();
            }

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
