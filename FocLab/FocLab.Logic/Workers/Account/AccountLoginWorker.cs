using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Settings.Statics;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FocLab.Logic.Workers.Account
{
    public class AccountLoginWorker : FocLabWorker
    {
        UserSearcher UserSearcher { get; }
        UserWorker UserWorker { get; }
        IApplicationAuthenticationManager AuthenticationManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }

        public AccountLoginWorker(ICrocoAmbientContextAccessor context,
            ICrocoApplication application,
            UserSearcher userSearcher,
            UserWorker userWorker,
            IApplicationAuthenticationManager authenticationManager,
            SignInManager<ApplicationUser> signInManager) : base(context, application)
        {
            UserSearcher = userSearcher;
            UserWorker = userWorker;
            AuthenticationManager = authenticationManager;
            SignInManager = signInManager;
        }

        #region Методы логинирования

        public async Task<BaseApiResponse<LoginResultModel>> LoginByPhoneNumberAsync(LoginByPhoneNumberModel model)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<LoginResultModel>(validation);
            }

            var user = await UserSearcher.GetUserByPhoneNumberAsync(model.PhoneNumber);

            if (user == null)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Пользователь не найден по указанному номеру телефона");
            }

            return await LoginAsync(new LoginModel(model, user.Email));
        }

        public async Task<BaseApiResponse<LoginResultModel>> LoginAsync(LoginModel model)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<LoginResultModel>(validation);
            }

            if (IsAuthenticated)
            {
                return new BaseApiResponse<LoginResultModel>(false, "Вы уже авторизованы в системе", new LoginResultModel { Result = LoginResult.AlreadyAuthenticated });
            }

            model.RememberMe = true;

            var result = false;

            var user = await SignInManager.UserManager.FindByEmailAsync(model.Email);

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

            try
            {
                //проверяю пароль
                var passCheckResult = await UserWorker.CheckUserNameAndPasswordAsync(user.Id, user.UserName, model.Password);

                //если пароль не подходит выдаю ответ
                if (!passCheckResult.IsSucceeded)
                {
                    return new BaseApiResponse<LoginResultModel>(false, "Неудачная попытка входа", new LoginResultModel { Result = LoginResult.UnSuccessfulAttempt, TokenId = null });
                }
                
                if (user.Email == RightsSettings.RootEmail) //root входит без подтверждений
                {
                    await SignInManager.SignInAsync(user, model.RememberMe);

                    return new BaseApiResponse<LoginResultModel>(true, "Вы успешно авторизованы", new LoginResultModel { Result = LoginResult.SuccessfulLogin });
                }

                if (AccountSettings.ConfirmLogin == ConfirmLoginType.None) //не логинить пользователя если нужно подтвержать логин
                {
                    await SignInManager.SignInAsync(user, model.RememberMe);

                    result = true;
                }


            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "");

                return new BaseApiResponse<LoginResultModel>(false, ex.Message);
            }

            
            if (result)
            {   
                return new BaseApiResponse<LoginResultModel>(true, "Авторизация прошла успешно", new LoginResultModel { Result = LoginResult.SuccessfulLogin, TokenId = null });
            }

            return new BaseApiResponse<LoginResultModel>(false, "Неудачная попытка входа", new LoginResultModel { Result = LoginResult.UnSuccessfulAttempt, TokenId = null });
        }

        public async Task<BaseApiResponse> LoginAsUserAsync(UserIdModel model)
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

            var user = await SignInManager.UserManager.FindByIdAsync(model.Id);

            if(user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по укаанному идентификатору");
            }

            await SignInManager.SignInAsync(user, true);

            return new BaseApiResponse(true, $"Вы залогинены как {user.Email}");
        }
        #endregion

        /// <summary>
        /// Разлогинивание в системе
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationManager"></param>
        /// <returns></returns>
        public BaseApiResponse LogOut(IPrincipal user)
        {
            if(!user.Identity.IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы и так не авторизованы");
            }

            AuthenticationManager.SignOut();

            return new BaseApiResponse(true, "Вы успешно разлогинены в системе");
        }
    }
}