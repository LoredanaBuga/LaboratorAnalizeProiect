using System;
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

        [BindProperty]
        public Pacient PacientInput { get; set; } = new Pacient();

        [BindProperty]
        public Programare Programare { get; set; } = new Programare();

        public IActionResult OnGet()
        {
            ViewData["PachetAnalizeID"] = new SelectList(_context.PachetAnalize, "ID", "Denumire");

            if (User.IsInRole("Admin"))
            {
                ViewData["PacientID"] = new SelectList(_context.Pacient, "ID", "NumeComplet");
            }
            else
            {
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
            // 1. Legăm analizele selectate
            if (selectedTipuriAnalize != null)
            {
                Programare.ProgramareTipAnalize = new List<ProgramareTipAnaliza>();
                foreach (var a in selectedTipuriAnalize)
                {
                    Programare.ProgramareTipAnalize.Add(new ProgramareTipAnaliza { TipAnalizaID = int.Parse(a) });
                }
            }

            Programare.UserId = _userManager.GetUserId(User);

            // 2. LOGICA PENTRU ADMIN (Păcălim validarea)
            if (User.IsInRole("Admin"))
            {
                // Completăm valorile manual ca să nu mai apară mesajele roșii de "Required"
                PacientInput.Nume = "ADMIN";
                PacientInput.Prenume = "ADMIN";
                PacientInput.CNP = "0000000000000";
                PacientInput.Telefon = "0000000000";
                PacientInput.Email = "admin@test.com";

                // Ștergem orice eroare de validare rămasă pentru PacientInput
                ModelState.ClearValidationState("PacientInput");
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith("PacientInput")).ToList())
                {
                    ModelState.Remove(key);
                }
            }
            else
            {
                // Logica pentru client normal
                var uid = Programare.UserId;

                // Dacă datele de client sunt invalide, ne oprim aici
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
                Programare.PacientID = pacient.ID;
            }

            // Eliminăm erorile de navigare (obiectele mari) care blochează salvarea
            ModelState.Remove("Programare.Pacient");
            ModelState.Remove("Programare.PachetAnalize");

            // 3. SALVARE FINALĂ
            // Dacă e Admin și avem PacientID, forțăm salvarea chiar dacă ModelState are erori reziduale
            if (User.IsInRole("Admin") && Programare.PacientID != null)
            {
                _context.Programare.Add(Programare);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Dacă e client și modelul e invalid, întoarcem pagina
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