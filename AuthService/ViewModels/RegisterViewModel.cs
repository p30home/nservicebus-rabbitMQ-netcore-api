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

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        public override string ToString()
        {
            return $@"{nameof(RegisterViewModel)}@
            {nameof(FirstName)}:{FirstName}
            {nameof(LastName)}:{LastName}
            {nameof(Email)}:{Email}
            {nameof(Password)}:*****";
        }
    }
}