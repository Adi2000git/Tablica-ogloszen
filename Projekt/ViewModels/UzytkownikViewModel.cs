using System.ComponentModel.DataAnnotations;

namespace Projekt.ViewModels
{
    public class UzytkownikViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Haslo { get; set; }

        [Required]
        [Compare("Haslo")]
        [Display(Name = "Powtórz Hasło")]
        [DataType(DataType.Password)]
        public string PowtorzHaslo { get; set; }

        [Required]
        public string Rola { get; set; }


    }
}
