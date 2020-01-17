using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FocLab.Logic.Extensions;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FocLab.Logic.Services
{

    // Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public ApplicationUser GetCachedUser(ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = claimsPrincipal.GetUserId();

            return SourceSettings.UseInternalCaching ? Users.Cached().FirstOrDefault(x => x.Id == userId) : Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}