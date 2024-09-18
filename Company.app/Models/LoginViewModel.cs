using System.ComponentModel.DataAnnotations;

namespace Company.app.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "invaild format for email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
