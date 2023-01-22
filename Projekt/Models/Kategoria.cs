using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Kategoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nazwa { get; set; }

        public virtual ICollection<Ogloszenie> Ogloszenia { get; set; } = new HashSet<Ogloszenie>();
    }
}
