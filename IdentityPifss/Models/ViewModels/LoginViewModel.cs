using System.ComponentModel.DataAnnotations;

namespace IdentityPifss.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter Password ")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
