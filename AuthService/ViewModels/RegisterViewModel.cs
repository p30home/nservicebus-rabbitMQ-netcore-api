using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required(AllowEmptyStrings=false)]
        public string Password { get; set; }
    }
}