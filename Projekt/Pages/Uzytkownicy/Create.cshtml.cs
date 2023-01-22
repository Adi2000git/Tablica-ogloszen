using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.DATA;
using Projekt.Models;
using Projekt.ViewModels;

namespace Projekt.Pages.Uzytkownicy
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
            Role = new SelectList(new List<string>() { "Administrator", "Moderator", "Użytkownik" });
            return Page();
        }

        [BindProperty]
        public UzytkownikViewModel Uzytkownik { get; set; } = default!;

        public SelectList Role { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Uzytkownik == null || Uzytkownik == null)
            {
                Role = new SelectList(new List<string>() { "Administrator", "Moderator", "Użytkownik" });
                return Page();
            }
            string userpassword = Uzytkownik.Haslo;
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userpassword);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                userpassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToLower();

            }
            Uzytkownik uzytkownik = new Uzytkownik()
            {
                Haslo = userpassword,
                Login = Uzytkownik.Login,
                Rola = Uzytkownik.Rola
            };
            _context.Uzytkownik.Add(uzytkownik);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
