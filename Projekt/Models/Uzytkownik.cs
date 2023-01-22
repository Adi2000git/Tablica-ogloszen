using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Uzytkownik
    {    
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        [StringLength(128)]
        public string Haslo { get; set; }

        [Required]
        [StringLength(20)]
        public string Rola { get; set; }
    }
}
