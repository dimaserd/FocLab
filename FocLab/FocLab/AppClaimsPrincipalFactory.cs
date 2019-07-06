using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FocLab.Consts;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FocLab
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity) principal.Identity).AddClaims(new List<Claim>
            {
                new Claim(nameof(user.AvatarFileId), user.AvatarFileId.ToString()),
                new Claim(ApplicationUserClaims.Email, user.Email),
                new Claim(ApplicationUserClaims.AvatarFileId, user.AvatarFileId.ToString()),
                new Claim(ApplicationUserClaims.Name, user.Name)
            });

            return principal;
        }
    }
}
