using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmed password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmedPassword { get; set; }
    }
}
