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

namespace Projekt.Pages.Kategorie
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public CreateModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Kategoria Kategoria { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Kategoria == null || Kategoria == null)
            {
                return Page();
            }

            _context.Kategoria.Add(Kategoria);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
