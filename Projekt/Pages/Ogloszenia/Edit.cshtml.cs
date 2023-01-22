using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.DATA;
using Projekt.Models;

namespace Projekt.Pages.Ogloszenia
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public EditModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ogloszenie Ogloszenie { get; set; } = default!;
        public SelectList Kategorie { get; set; } = default!;

        [BindProperty]
        public List<int> KategorieId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ogloszenie == null)
            {
                return NotFound();
            }

            var ogloszenie =  await _context.Ogloszenie.Include(x => x.Uzytkownik).Include(x=>x.Kategorie).FirstOrDefaultAsync(m => m.Id == id);
            if (ogloszenie == null || (ogloszenie.Uzytkownik.Login != User.Identity.Name && User.IsInRole("Uzytkownik")))
            {
                return NotFound();
            }
            Ogloszenie = ogloszenie;
            KategorieId = ogloszenie.Kategorie.Select(x => x.Id).ToList();
            Kategorie = new SelectList(_context.Kategoria, "Id", "Nazwa");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Ogloszenie.Uzytkownik");
            ModelState.Remove("DataDodania");

            if (!ModelState.IsValid || _context.Ogloszenie == null || Ogloszenie == null || KategorieId == null || KategorieId.Count == 0)
            {
                return Page();
            }

            var ogloszenie = await _context.Ogloszenie.AsNoTracking().Include(x => x.Kategorie).Include(x => x.Uzytkownik).FirstOrDefaultAsync(m => m.Id == Ogloszenie.Id);
            var wybraneKategorie = _context.Kategoria.Where(x => KategorieId.Contains(x.Id)).ToList();

            _context.Attach(Ogloszenie).State = EntityState.Modified;

            Ogloszenie.Kategorie = (await _context.Ogloszenie.Include(x => x.Kategorie).FirstOrDefaultAsync(m => m.Id == Ogloszenie.Id)).Kategorie;
            foreach (var item in KategorieId.Where(x => !Ogloszenie.Kategorie.Where(y => y.Id == x).Any()))
            {
                Ogloszenie.Kategorie.Add(wybraneKategorie.Where(x => x.Id == item).FirstOrDefault());
            }
            var kategoriedousuniecia = Ogloszenie.Kategorie.Where(x => !KategorieId.Where(y => y == x.Id).Any()).ToList();

            foreach (var item in kategoriedousuniecia)
            {
                Ogloszenie.Kategorie.Remove(Ogloszenie.Kategorie.Where(x => x.Id == item.Id).FirstOrDefault());
            }

            
            Ogloszenie.DataDodania = ogloszenie.DataDodania;
            Ogloszenie.UzytkownikId = ogloszenie.UzytkownikId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OgloszenieExists(Ogloszenie.Id))
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

        private bool OgloszenieExists(int id)
        {
          return (_context.Ogloszenie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
