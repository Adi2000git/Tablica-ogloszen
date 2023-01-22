using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.DATA;
using Projekt.Models;

namespace Projekt.Pages.Ogloszenia
{
    public class IndexModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public IndexModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }

        public IList<Ogloszenie> Ogloszenie { get;set; } = default!;

        public IList<int> Ulubione { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Request.Cookies.TryGetValue("Ulubione", out string ulubione);
            Ulubione = new List<int>();
            if (ulubione != null && ulubione != "")
            {
                Ulubione = JsonSerializer.Deserialize<List<int>>(ulubione);
            }

            if (_context.Ogloszenie != null)
            {

                Ogloszenie = await _context.Ogloszenie
                .Include(o => o.Uzytkownik).Include(o => o.Kategorie).ToListAsync();
            }
        }
    }
}
