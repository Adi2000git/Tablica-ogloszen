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
    public class IndexModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public IndexModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        public IList<Kategoria> Kategoria { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Kategoria != null)
            {
                Kategoria = await _context.Kategoria.ToListAsync();
            }
        }
    }
}
