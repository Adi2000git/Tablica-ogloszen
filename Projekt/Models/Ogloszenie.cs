using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Ogloszenie
    {
        public int Id { get; set; }

        public int UzytkownikId { get; set; }

        [Required]
        [StringLength(30)]
        public string Tytul { get; set; }

        [Required]
        [StringLength(100)]
        public string Opis { get; set; }

        public DateTime DataDodania { get; set; }
        
        public virtual Uzytkownik Uzytkownik { get; set; } 
        public virtual ICollection<Kategoria> Kategorie { get; set; } = new List<Kategoria>();
    }
}
