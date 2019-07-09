using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Account
{
    public class AccountRegistrationWorker : BaseChemistryWorker
    {
        #region Методы регистрации
        
        /// <summary>
        /// Метод регистрирующий пользователя администратором
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userManager"></param>
        /// <param name="userRights"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<ApplicationUserDto>> RegisterUserByAdminAsync(RegisterModel model, ApplicationUserManager userManager, List<UserRight> userRights)
        {
            var validation = ValidateModel(model);
            
            if(!validation.IsSucceeded)
            {
                return new BaseApiResponse<ApplicationUserDto>(validation);
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse<ApplicationUserDto>(false, "Вы не можете регистрировать пользователей, так как вы не авторизованы в системе");
            }

            if (!User.IsAdmin())
            {
                return new BaseApiResponse<ApplicationUserDto>(false, "Вы не можете регистрировать пользователей так, как вы не являетесь администратором");
            }

            var result = await RegisterHelpMethodAsync(model, userManager, userRights);

            if (!result.IsSucceeded)
            {
                return result;
            }

            var user = result.ResponseObject;
            
            
            var message = "Вы успешно зарегистрировали пользователя.";

            return new BaseApiResponse<ApplicationUserDto>(true, message, user);
        }

        private async Task<BaseApiResponse<ApplicationUserDto>> RegisterHelpMethodAsync(RegisterModel model, ApplicationUserManager userManager, IReadOnlyCollection<UserRight> userRights)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                UserName = model.Email,
                CreatedOn = DateTime.Now,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var checkResult = await CheckUserAsync(user);

            if (!checkResult.IsSucceeded)
            {
                return new BaseApiResponse<ApplicationUserDto>(checkResult.IsSucceeded, checkResult.Message);
            }

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new BaseApiResponse<ApplicationUserDto>(false, result.Errors.ToList().First().Description);
            }

            userManager.AddRight(user, UserRight.Customer);

            if (userRights == null)
            {
                return new BaseApiResponse<ApplicationUserDto>(true, "Пользователь создан", user.ToDto());
            }
                
            foreach (var right in userRights.Where(x => x != UserRight.SuperAdmin && x != UserRight.Admin && x != UserRight.Root && x != UserRight.Seller))
            {
                userManager.AddRight(user, right);
            }

            return new BaseApiResponse<ApplicationUserDto>(true, "Пользователь создан", user.ToDto());
        }

        #endregion

        private async Task<BaseApiResponse> CheckUserAsync(ApplicationUser user)
        {
            if(string.IsNullOrWhiteSpace(user.Email))
            {
                return new BaseApiResponse(false, "Вы не указали адрес электронной почты");
            }

            if(string.IsNullOrWhiteSpace(user.Name))
            {
                return new BaseApiResponse(false, "Имя не может быть пустым");
            }

            if(string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                return new BaseApiResponse(false, "Вы не указали номер телефона");
            }

            var userRepo = GetRepository<ApplicationUser>();

            if (await userRepo.Query().AnyAsync(x => x.Email == user.Email))
            {
                return new BaseApiResponse(false, "Пользователь с данным email адресом уже существует");
            }

            if (await userRepo.Query().AnyAsync(x => x.PhoneNumber == user.PhoneNumber))
            {
                return new BaseApiResponse(false, "Пользователь с данным номером телефона уже существует");
            }

            return new BaseApiResponse(true, "");
        }

        public AccountRegistrationWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}
