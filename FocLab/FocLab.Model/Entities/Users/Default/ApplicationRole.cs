using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Model.Entities.Users.Default
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
