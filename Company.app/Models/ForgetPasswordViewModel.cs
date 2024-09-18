using System.ComponentModel.DataAnnotations;

namespace Company.app.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "invaild format for email")]
        public string Email { get; set; }
    }
}
