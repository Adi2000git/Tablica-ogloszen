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
using Microsoft.EntityFrameworkCore;
using Projekt.DATA;
using Projekt.Models;
using Projekt.ViewModels;

namespace Projekt.Pages.Uzytkownicy
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public EditModel(Projekt.DATA.OgloszeniaContext context)
        {
            Role = new SelectList(new List<string>() { "Administrator", "Moderator", "Użytkownik" });
            _context = context;
        }

        [BindProperty]
        public EdycjaUzytkownikViewModel Uzytkownik { get; set; } = default!;

        public SelectList Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik =  await _context.Uzytkownik.FirstOrDefaultAsync(m => m.Id == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            Uzytkownik = new EdycjaUzytkownikViewModel()
            {
                Login = uzytkownik.Login,
                Rola = uzytkownik.Rola,
                Id = uzytkownik.Id
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Role = new SelectList(new List<string>() { "Administrator", "Moderator", "Użytkownik" });
                return Page();
            }

            var uzytkownik = await _context.Uzytkownik.FirstOrDefaultAsync(m => m.Id == Uzytkownik.Id);
            uzytkownik.Rola = Uzytkownik.Rola;
            if(Uzytkownik.Haslo != null && Uzytkownik.Haslo != "")
            {
                string userpassword = Uzytkownik.Haslo;
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(userpassword);
                    byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                    userpassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToLower();

                }
                uzytkownik.Haslo = userpassword;
            }
            _context.Attach(uzytkownik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UzytkownikExists(Uzytkownik.Id))
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

        private bool UzytkownikExists(int id)
        {
          return (_context.Uzytkownik?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
