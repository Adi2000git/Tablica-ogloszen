using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Projekt.Pages.Ogloszenia
{
    public class UlubioneModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            Request.Cookies.TryGetValue("Ulubione", out string ulubione);
            List<int> idogloszenie= new List<int>();
            if (ulubione != null && ulubione != "")
            {
                idogloszenie = JsonSerializer.Deserialize<List<int>>(ulubione);
            }
            idogloszenie.Add(id);
            Response.Cookies.Append("Ulubione", JsonSerializer.Serialize(idogloszenie),new CookieOptions() { Expires = DateTime.Now.AddDays(30)});
            return RedirectToPage("Index");
        }
    }
}
