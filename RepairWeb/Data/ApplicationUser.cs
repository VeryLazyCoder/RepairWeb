using Microsoft.AspNetCore.Identity;

namespace RepairWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImageURL { get; set; }
    }
}
