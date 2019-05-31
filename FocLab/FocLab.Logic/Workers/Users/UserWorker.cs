using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using Croco.Core.Model.Entities.Store;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Resources;
using FocLab.Logic.Services;
using FocLab.Logic.Settings;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUserDto = FocLab.Logic.EntityDtos.Users.Default.ApplicationUserDto;

namespace FocLab.Logic.Workers.Users
{
    public class UserWorker : BaseChemistryWorker
    {
        
        #region Изменение пароля
        public async Task<BaseApiResponse> ChangePasswordAsync(ResetPasswordByAdminModel model, ApplicationUserManager userManager)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден");
            }

            var searcher = new UserSearcher(ApplicationContextWrapper);

            var userDto = await searcher.GetUserByIdAsync(user.Id);

            var result = UserRightsWorker.HasRightToEditUser(userDto, ContextWrapper.User);

            if (!result.IsSucceeded)
            {
                return result;
            }

            return await ChangePasswordBaseAsync(model, userManager);
        }

        /// <summary>
        /// Данный метод не может быть вынесен в API (Базовый метод)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> ChangePasswordBaseAsync(ResetPasswordByAdminModel model, ApplicationUserManager userManager)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден");
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);

            var resetResult = await userManager.ResetPasswordAsync(user, code, model.Password);

            if (!resetResult.Succeeded)
            {
                return new BaseApiResponse(resetResult.Succeeded, resetResult.Errors.First().Description);
            }

            return new BaseApiResponse(true, $"Вы изменили пароль для пользователя {user.Email}");
        }
        #endregion


        public async Task<BaseApiResponse> CheckUserNameAndPasswordAsync(string userId, string userName, string pass)
        {
            var userRepo = ContextWrapper.GetRepository<ApplicationUser>();

            var user = await userRepo.Query()
                .FirstOrDefaultAsync(x => x.Id == userId);

            var passHasher = new PasswordHasher<ApplicationUser>();
            
            var t = passHasher.VerifyHashedPassword(user, user.PasswordHash, pass) != PasswordVerificationResult.Failed && user.UserName == userName;

            return new BaseApiResponse(t, "");
        }

        public async Task<List<ApplicationUserDto>> GetUsersAsync()
        {
            return await ContextWrapper.GetRepository<ApplicationUser>().Query().Select(x => new ApplicationUserDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<BaseApiResponse> RemoveUserAsync(UserIdModel model)
        {
            if(!RightsSettings.UserRemovingEnabled)
            {
                return new BaseApiResponse(false, "В настройках вашего приложения выключена опция удаления пользователей");
            }
            
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if(!User.HasRight(UserRight.Root))
            {
                return new BaseApiResponse(false, "Вы не имеете прав для удаления пользователя");
            }

            var searcher = new UserSearcher(ApplicationContextWrapper);

            var userToRemove = await searcher.GetUserByIdAsync(model.Id);

            if(userToRemove == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору");
            }

            if(userToRemove.Rights.Any(x => x == UserRight.Root))
            {
                return new BaseApiResponse(false, "Вы не можете удалить Root пользователя");
            }

            var userId = model.Id;

            var db = Context;

            db.Set<PageLogAction>().RemoveRange(db.Set<PageLogAction>().Where(x => x.UserId == userId));

            

            db.UserTokens.RemoveRange(db.UserTokens.Where(x => x.UserId == userId));

            db.Users.RemoveRange(db.Users.Where(x => x.Id == userId));

            return await TrySaveChangesAndReturnResultAsync($"Пользователь {userToRemove.Email} удален");
        }


        /// <summary>
        /// Редактирование пользователя администратором
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> EditUserAsync(EditApplicationUser model)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return validation;
            }

            //если пользователь не является администратором и не является самим собой
            if(!User.IsAdmin())
            {
                return new BaseApiResponse(false, ValidationMessages.YouAreNotAnAdministrator);
            }

            var userRepo = ContextWrapper.GetRepository<ApplicationUser>();

            var searcher = new UserSearcher(ApplicationContextWrapper);

            var userDto = await searcher.GetUserByIdAsync(model.Id);
            
            if (userDto == null)
            {
                return new BaseApiResponse(false, ValidationMessages.UserIsNotFoundByIdentifier);
            }

            if (userDto.Email == RightsSettings.RootEmail)
            {
                return new BaseApiResponse(false, ValidationMessages.YouCantEditRootUser);
            }

            if(await userRepo.Query().AnyAsync(x => x.Email == model.Email && x.Id != model.Id))
            {
                return new BaseApiResponse(false, ValidationMessages.ThisEmailIsAlreadyTaken);
            }
            
            if(await userRepo.Query().AnyAsync(x => x.PhoneNumber == model.PhoneNumber && x.Id != model.Id))
            {
                return new BaseApiResponse(false, ValidationMessages.ThisPhoneNumberIsAlreadyTaken);
            }
            

            if(!User.HasRight(UserRight.Root) && (userDto.Rights.Any(x => x == UserRight.Admin) || userDto.Rights.Any(x => x == UserRight.SuperAdmin)))
            {
                return new BaseApiResponse(false, ValidationMessages.YouCantEditUserBecauseHeIsAdministrator);
            }

            if(!User.HasRight(UserRight.Root) && User.HasRight(UserRight.SuperAdmin) && userDto.Rights.Any(x => x == UserRight.SuperAdmin))
            {
                return new BaseApiResponse(false, ValidationMessages.YouCantEditUserBecauseHeIsSuperAdministrator);
            }

            if (!User.HasRight(UserRight.Root) && !User.HasRight(UserRight.SuperAdmin) && User.HasRight(UserRight.Admin) && userDto.Rights.Any(x => x == UserRight.Admin))
            {
                return new BaseApiResponse(false, "Вы не имеете прав Супер-Администратора, следовательно не можете редактировать пользователя, так как он является Администратором");
            }

            var userToEditEntity = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (userToEditEntity == null)
            {
                var logger = Context.GetLogger();

                await logger.LogExceptionAsync(new Exception("Ужасная ошибка"));

                return new BaseApiResponse(false, "Ужасная ошибка");
            }

            
            userToEditEntity.Email = model.Email;
            userToEditEntity.Name = model.Name;
            userToEditEntity.Surname = model.Surname;
            userToEditEntity.Patronymic = model.Patronymic;
            userToEditEntity.Sex = model.Sex;
            userToEditEntity.ObjectJson = model.ObjectJson;
            userToEditEntity.PhoneNumber = new string(model.PhoneNumber.Where(char.IsDigit).ToArray());
            userToEditEntity.BirthDate = model.BirthDate;

            userRepo.UpdateHandled(userToEditEntity);

            return await TrySaveChangesAndReturnResultAsync("Данные пользователя обновлены");
        }

        
        public async Task<BaseApiResponse> ActivateOrDeActivateUserAsync(UserActivation model)
        {
            var searcher = new UserSearcher(ApplicationContextWrapper);

            var userDto = await searcher.GetUserByIdAsync(model.Id);

            if(userDto == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору");
            }
            
            var result = UserRightsWorker.HasRightToEditUser(userDto, ContextWrapper.User);
            
            if(!result.IsSucceeded)
            {
                return result;
            }

            var userRepo = GetRepository<ApplicationUser>();

            var user = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору");
            }

            if (model.DeActivated)
            {
                if(user.DeActivated)
                {
                    return new BaseApiResponse(false, "Пользователь уже является деактивированным");
                }

                
                user.DeActivated = true;

                userRepo.UpdateHandled(user);

                return await TrySaveChangesAndReturnResultAsync("Пользователь деактивирован");
            }

            if(!user.DeActivated)
            {
                return new BaseApiResponse(false, "Пользователь уже активирован");
            }

            
            user.DeActivated = false;

            userRepo.UpdateHandled(user);

            return await TrySaveChangesAndReturnResultAsync("Пользователь активирован");
        }

        public UserWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}
