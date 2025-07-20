using Microsoft.AspNetCore.Identity;

namespace DemoMVC.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; } = default!;
    }
}