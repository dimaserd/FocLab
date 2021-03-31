using Microsoft.AspNetCore.Identity;

namespace FocLab.Model.Entities.Users.Default
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}