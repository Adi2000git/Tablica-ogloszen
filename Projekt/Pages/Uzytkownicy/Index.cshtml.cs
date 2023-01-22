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
    public class IndexModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public IndexModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        public IList<Uzytkownik> Uzytkownik { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Uzytkownik != null)
            {
                Uzytkownik = await _context.Uzytkownik.ToListAsync();
            }
        }
    }
}
