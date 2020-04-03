namespace AuthService.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return $@"{nameof(LoginViewModel)}@
            {nameof(Email)}:{Email}
            {nameof(Password)}:*****";
        }
    }
}