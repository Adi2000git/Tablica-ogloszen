using System.ComponentModel.DataAnnotations;

namespace Projekt.ViewModels
{
    public class EdycjaUzytkownikViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string? Haslo { get; set; }

        [Compare("Haslo")]
        [Display(Name = "Powtórz Hasło")]
        [DataType(DataType.Password)]
        public string? PowtorzHaslo { get; set; }

        [Required]
        public string Rola { get; set; }
    }
}
