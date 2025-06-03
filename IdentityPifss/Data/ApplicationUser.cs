using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace IdentityPifss.Data
{
    public class ApplicationUser : IdentityUser
    {

        public string? City { get; set; }
        public Genders? Gender { get; set; }
    }
    public enum Genders { Male, Female }
}
