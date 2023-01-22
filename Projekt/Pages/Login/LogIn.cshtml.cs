using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;
using Projekt.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Projekt.Pages.Login
{
    public class LogInModel : PageModel
    {
        
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        private readonly Projekt.DATA.OgloszeniaContext _context;

        public LogInModel(Projekt.DATA.OgloszeniaContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        private Uzytkownik ValidateUser(SiteUser user)
        {
            var uzytkownik = _context.Uzytkownik.FirstOrDefault(x=>x.Login == user.userName);
            bool isValid = false;
            if (uzytkownik != null)
            {
                string password = uzytkownik.Haslo;
                string userpassword = user.password;
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(userpassword);
                    byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                    userpassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToLower();

                }
                if (password == userpassword)
                {
                    isValid = true;
                }

            }
            
            if(isValid)
            {
                return uzytkownik;
            }
            else
            {
                return null;
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var uzytkownik = ValidateUser(user);
            if (uzytkownik != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim(ClaimTypes.Role, uzytkownik.Rola)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
               ClaimsPrincipal(claimsIdentity));
                HttpContext.Session.SetInt32("UserId", uzytkownik.Id);
                return RedirectToPage("/Ogloszenia/Index");
            }
            return Page();
        }
    }
}
