using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Projekt.ViewModels
{
    public class SiteUser
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required]
       // [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",ErrorMessage ="Invalid Email Address")]
        public string? userName { get; set; }

        [Display(Name = "Hasło")]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
