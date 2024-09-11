using Company.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.app.Models
{
    public class SignUpViewModel 
    {
        [Required(ErrorMessage ="first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last name is required")]

        public string LastName { get; set; }
        [EmailAddress(ErrorMessage ="invaild format for email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one non-alphanumeric character.")]

        public string Password { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [Compare(nameof(Password) , ErrorMessage ="confirm password does not match password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "required to agree")]

        public bool IsAgree { get; set; }

    }
}
