using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Logic.Workers;
using Croco.Core.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Services;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Logic.Workers.Account
{
    public class AccountManager : BaseCrocoWorker
    {
        private async Task<BaseApiResponse> CreateRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = Enum.GetValues(typeof(UserRight)).Cast<UserRight>().Select(x => x.ToString()).ToArray();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role, ConcurrencyStamp = Guid.NewGuid().ToString() });
                }
            }

            return new BaseApiResponse(true, "Роли созданы");
        }

        /// <summary>
        /// Создается пользователь Root в системе и ему присваиваются все необходимые права
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> InitAsync(RoleManager<ApplicationRole> roleManager, ApplicationUserManager userManager)
        {
            await CreateRolesAsync(roleManager);
            
            var maybeRoot = await CreateOrUpdateRoot(userManager);

            foreach (UserRight right in Enum.GetValues(typeof(UserRight)))
            {
                userManager.AddRight(maybeRoot, right);
            }

            return new BaseApiResponse(true, "Пользователь root создан");
        }

        private async Task<ApplicationUser> CreateOrUpdateRoot(UserManager<ApplicationUser> userManager)
        {
            var maybeRoot = await userManager.FindByEmailAsync(RightsSettings.RootEmail);

            if (maybeRoot == null)
            {
                maybeRoot = new ApplicationUser
                {
                    Email = RightsSettings.RootEmail,
                    EmailConfirmed = true,
                    Name = "Root",
                    UserName = RightsSettings.RootEmail,
                    CreatedOn = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                await userManager.CreateAsync(maybeRoot, RightsSettings.RootPassword);
                return maybeRoot;
            }

            
            var userRepo = GetRepository<ApplicationUser>();

            var passHasher = new PasswordHasher<ApplicationUser>();

            var pass = passHasher.HashPassword(maybeRoot, RightsSettings.RootPassword);

            maybeRoot.PasswordHash = pass;

            userRepo.UpdateHandled(maybeRoot);

            await TrySaveChangesAndReturnResultAsync("Ok");

            return maybeRoot;
        }

        public BaseApiResponse<ApplicationUserDto> CheckUserChanges(IApplicationAuthenticationManager authenticationManager, SignInManager<ApplicationUser> signInManager)
        {
            if(!IsAuthenticated)
            {
                return new BaseApiResponse<ApplicationUserDto>(true, "Вы не авторизованы в системе", null);
            }

            return new BaseApiResponse<ApplicationUserDto>(true, "", null);
        }

        #region Методы изменения

        public async Task<BaseApiResponse> ChangePasswordAsync(ChangeUserPasswordModel model, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if(string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.OldPassword))
            {
                return new BaseApiResponse(false, "Новый пароль или старый пароль не указаны");
            }

            if(model.NewPassword == model.OldPassword)
            {
                return new BaseApiResponse(false, "Новый и старый пароль совпадют");
            }

            var userId = UserId;

            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return new BaseApiResponse(false, "Неправильно указан старый пароль");
            }

            if (user != null)
            {
                await signInManager.SignInAsync(user, true);
            }

            return new BaseApiResponse(true, "Ваш пароль изменен");
        }
        #endregion

        public AccountManager(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}
