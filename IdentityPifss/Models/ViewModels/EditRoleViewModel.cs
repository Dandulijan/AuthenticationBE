using System.ComponentModel.DataAnnotations;

namespace IdentityPifss.Models.ViewModels
{
    public class EditRoleViewModel
    {
        [Required(ErrorMessage = "Enter Role Name")]
        [MinLength(2)]
        public string RoleName { get; set; }

        public string RoleId { get; set; }
    }
}
