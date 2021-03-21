﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Services;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Account
{
    public class AccountManager : FocLabWorker
    {
        RoleManager<ApplicationRole> RoleManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }
        ApplicationUserManager UserManager { get; }

        public AccountManager(ICrocoAmbientContextAccessor contextAccessor, 
            ICrocoApplication application,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationUserManager userManager
            )
            : base(contextAccessor, application)
        {
            RoleManager = roleManager;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        private async Task<BaseApiResponse> CreateRolesAsync()
        {
            var roles = Enum.GetValues(typeof(UserRight)).Cast<UserRight>().Select(x => x.ToString()).ToArray();

            foreach (var role in roles)
            {
                if (!await RoleManager.RoleExistsAsync(role))
                {
                    await RoleManager.CreateAsync(new ApplicationRole { Name = role, ConcurrencyStamp = Guid.NewGuid().ToString() });
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
        public async Task<BaseApiResponse> InitAsync()
        {
            await CreateRolesAsync();
            
            var maybeRoot = await CreateOrUpdateRoot();

            foreach (UserRight right in Enum.GetValues(typeof(UserRight)))
            {
                UserManager.AddRight(maybeRoot, right);
            }

            return new BaseApiResponse(true, "Пользователь root создан");
        }

        private async Task<ApplicationUser> CreateOrUpdateRoot()
        {
            var maybeRoot = await UserManager.FindByEmailAsync(RightsSettings.RootEmail);

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

                await UserManager.CreateAsync(maybeRoot, RightsSettings.RootPassword);
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

        public BaseApiResponse<ApplicationUserDto> CheckUserChanges()
        {
            if(!IsAuthenticated)
            {
                return new BaseApiResponse<ApplicationUserDto>(true, "Вы не авторизованы в системе", null);
            }

            return new BaseApiResponse<ApplicationUserDto>(true, "", null);
        }

        #region Методы изменения

        public async Task<BaseApiResponse> OverrideUserPassword(OverrideUserPasswordModel model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if (string.IsNullOrEmpty(model.NewPassword))
            {
                return new BaseApiResponse(false, "Новый пароль не указаны");
            }

            var user = await UserManager
                    .Users.FirstOrDefaultAsync(x => x.Id == model.UserId);

            if(user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору");
            }

            var identityResult = await UserManager.RemovePasswordAsync(user);

            if (!identityResult.Succeeded)
            {
                return new BaseApiResponse(false, identityResult.Errors.First().Description);
            }

            identityResult = await UserManager.AddPasswordAsync(user, model.NewPassword);

            if (!identityResult.Succeeded)
            {
                return new BaseApiResponse(false, identityResult.Errors.First().Description);
            }

            return new BaseApiResponse(true, "Пароль успешно изменени");
        }

        public async Task<BaseApiResponse> ChangePasswordAsync(ChangeUserPasswordModel model)
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

            var user = await UserManager.FindByIdAsync(userId);

            var result = await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return new BaseApiResponse(false, "Неправильно указан старый пароль");
            }

            if (user != null)
            {
                await SignInManager.SignInAsync(user, true);
            }

            return new BaseApiResponse(true, "Ваш пароль изменен");
        }
        #endregion
    }
}