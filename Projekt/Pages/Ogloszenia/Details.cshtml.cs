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
    public class DetailsModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public DetailsModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

      public Ogloszenie Ogloszenie { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ogloszenie == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.FirstOrDefaultAsync(m => m.Id == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }
            else 
            {
                Ogloszenie = ogloszenie;
            }
            return Page();
        }
    }
}
