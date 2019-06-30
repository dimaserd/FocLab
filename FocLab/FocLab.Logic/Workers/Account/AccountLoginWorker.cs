using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Settings.Statics;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.Workers.Account
{
    public class AccountLoginWorker : BaseChemistryWorker
    {
        #region Методы логинирования

        public async Task<BaseApiResponse<LoginResultModel>> LoginByPhoneNumberAsync(LoginByPhoneNumberModel model, ApplicationSignInManager signInManager)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<LoginResultModel>(validation);
            }

            var searcher = new UserSearcher(ApplicationContextWrapper);

            var user = await searcher.GetUserByPhoneNumberAsync(model.PhoneNumber);

            if (user == null)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Пользователь не найден по указанному номеру телефона");
            }

            return await LoginAsync(new LoginModel(model, user.Email), signInManager);
        }

        public async Task<BaseApiResponse<LoginResultModel>> LoginAsync(LoginModel model, ApplicationSignInManager signInManager)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<LoginResultModel>(validation);
            }

            if (ContextWrapper.IsAuthenticated)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Вы уже авторизованы в системе", new LoginResultModel { Result = LoginResult.AlreadyAuthenticated });
            }

            model.RememberMe = true;

            var result = false;

            var user = await signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Неудачная попытка входа", new LoginResultModel { Result = LoginResult.UnSuccessfulAttempt });
            }

            if (user.DeActivated)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Ваша учетная запись деактивирована", new LoginResultModel { Result = LoginResult.UserDeactivated });
            }

            //если логинирование не разрешено для пользователей которые не подтвердили Email и у пользователя Email не потверждён
            if (user.Email != RightsSettings.RootEmail && !user.EmailConfirmed && !AccountSettings.IsLoginEnabledForUsersWhoDidNotConfirmEmail)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Ваш Email не подтверждён", new LoginResultModel { Result = LoginResult.EmailNotConfirmed });
            }

            var db = Context;

            try
            {
                var userWorker = new UserWorker(ApplicationContextWrapper);

                //проверяю пароль
                var passCheckResult = await userWorker.CheckUserNameAndPasswordAsync(user.Id, user.UserName, model.Password);

                //если пароль не подходит выдаю ответ
                if (!passCheckResult.IsSucceeded)
                {
                    return new BaseApiResponse<LoginResultModel>(false, "Неудачная попытка входа", new LoginResultModel { Result = LoginResult.UnSuccessfulAttempt, TokenId = null });
                }
                
                if (user.Email == RightsSettings.RootEmail) //root входит без подтверждений
                {
                    await signInManager.SignInAsync(user, model.RememberMe);

                    return new BaseApiResponse<LoginResultModel>(true, "Вы успешно авторизованы", new LoginResultModel { Result = LoginResult.SuccessfulLogin });
                }

                if (AccountSettings.ConfirmLogin == ConfirmLoginType.None) //не логинить пользователя если нужно подтвержать логин
                {
                    await signInManager.SignInAsync(user, model.RememberMe);

                    result = true;
                }


            }
            catch (Exception ex)
            {
                var logger = db.GetLogger();

                await logger.LogExceptionAsync(ex);

                return new BaseApiResponse<LoginResultModel>(false, ex.Message);
            }

            
            if (result)
            {   
                return new BaseApiResponse<LoginResultModel>(true, "Авторизация прошла успешно", new LoginResultModel { Result = LoginResult.SuccessfulLogin, TokenId = null });
            }

            return new BaseApiResponse<LoginResultModel>(false, "Неудачная попытка входа", new LoginResultModel { Result = LoginResult.UnSuccessfulAttempt, TokenId = null });
        }

        public async Task<BaseApiResponse> LoginAsUserAsync(UserIdModel model, ApplicationSignInManager signInManager)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return validation;
            }
            
            if(!User.HasRight(UserRight.Root))
            {
                return new BaseApiResponse(false, "У вас недостаточно прав для логинирования за другого пользователя");
            }

            var user = await signInManager.UserManager.FindByIdAsync(model.Id);

            if(user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по укаанному идентификатору");
            }

            await signInManager.SignInAsync(user, true);

            return new BaseApiResponse(true, $"Вы залогинены как {user.Email}");
        }
        #endregion

        /// <summary>
        /// Разлогинивание в системе
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationManager"></param>
        /// <returns></returns>
        public BaseApiResponse LogOut(IPrincipal user, IApplicationAuthenticationManager authenticationManager)
        {
            if(!user.Identity.IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы и так не авторизованы");
            }

            authenticationManager.SignOut();

            return new BaseApiResponse(true, "Вы успешно разлогинены в системе");
        }

        public AccountLoginWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}
