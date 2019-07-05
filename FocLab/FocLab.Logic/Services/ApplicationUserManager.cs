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
            var userId = claimsPrincipal.GetUserId();

            if (SourceSettings.UseInternalCaching)
            {
                return Users.Cached().FirstOrDefault(x => x.Id == userId);
            }

            return Store.FindByIdAsync(userId, CancellationToken).GetAwaiter().GetResult();
        }
    }
}
