using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class RegisterViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required(AllowEmptyStrings=false)]
        public string Password { get; set; }
    }
}