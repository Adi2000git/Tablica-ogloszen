using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.DATA;
using Projekt.Models;

namespace Projekt.Pages.Ogloszenia
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public DeleteModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Ogloszenie Ogloszenie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ogloszenie == null )
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.Include(x=>x.Uzytkownik).FirstOrDefaultAsync(m => m.Id == id);

            if (ogloszenie == null || (ogloszenie.Uzytkownik.Login != User.Identity.Name && User.IsInRole("Uzytkownik")))
            {
                return NotFound();
            }
            else 
            {
                Ogloszenie = ogloszenie;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Ogloszenie == null)
            {
                return NotFound();
            }
            var ogloszenie = await _context.Ogloszenie.FindAsync(id);

            if (ogloszenie != null)
            {
                Ogloszenie = ogloszenie;
                _context.Ogloszenie.Remove(Ogloszenie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
