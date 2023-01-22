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

namespace Projekt.Pages.Kategorie
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public DetailsModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

      public Kategoria Kategoria { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoria.FirstOrDefaultAsync(m => m.Id == id);
            if (kategoria == null)
            {
                return NotFound();
            }
            else 
            {
                Kategoria = kategoria;
            }
            return Page();
        }
    }
}
