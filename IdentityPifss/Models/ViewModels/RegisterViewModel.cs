using System.ComponentModel.DataAnnotations;
using IdentityPifss.Data;
namespace IdentityPifss.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Mobile { get; set; }
        
        public string? City { get; set; }

        public  Genders? Gender { get; set; }

     
    }
}
