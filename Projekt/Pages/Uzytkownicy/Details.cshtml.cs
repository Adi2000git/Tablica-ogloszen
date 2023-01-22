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

namespace Projekt.Pages.Uzytkownicy
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public DetailsModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

      public Uzytkownik Uzytkownik { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik.FirstOrDefaultAsync(m => m.Id == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            else 
            {
                Uzytkownik = uzytkownik;
            }
            return Page();
        }
    }
}
