using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.DATA;
using Projekt.Models;

namespace Projekt.Pages.Ogloszenia
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public CreateModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {       
            Kategorie = new SelectList(_context.Kategoria, "Id", "Nazwa");
            return Page();
        }

        [BindProperty]
        public Ogloszenie Ogloszenie { get; set; } = default!;


        public SelectList Kategorie { get; set; } = default!;

        [BindProperty]
        public List<int> KategorieId { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Ogloszenie.Uzytkownik");
            ModelState.Remove("DataDodania");

          if (!ModelState.IsValid || _context.Ogloszenie == null || Ogloszenie == null || KategorieId == null || KategorieId.Count == 0)
            {
                Kategorie = new SelectList(_context.Kategoria, "Id", "Nazwa");
                return Page();
            }

          var wybraneKategorie = _context.Kategoria.Where(x => KategorieId.Contains(x.Id)).ToList();
           // var uzytkownik = _context.Uzytkownik.Single(x=>x.Login == User.Identity.Name);
            Ogloszenie.Kategorie = wybraneKategorie;
            Ogloszenie.DataDodania = DateTime.Now;
            Ogloszenie.UzytkownikId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Ogloszenie.Add(Ogloszenie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
