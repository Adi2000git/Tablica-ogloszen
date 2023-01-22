using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projekt.DATA;
using Projekt.Models;

namespace Projekt.Pages.Kategorie
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly Projekt.DATA.OgloszeniaContext _context;
        public IConfiguration _configuration { get; }
        public DeleteModel(Projekt.DATA.OgloszeniaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string myCompanyDBcs = _configuration.GetConnectionString("OgloszeniaContext");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("DELETE FROM Kategoria WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@Id", id);// uzupełniane są na podstawie danych z kolekcji Parameters.
            con.Open();
            cmd.ExecuteNonQuery();//Metoda która jest wywoływana w wypadku poleceń innych niż SELECT
            con.Close();
          

            return RedirectToPage("./Index");
        }
    }
}
