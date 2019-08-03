using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Account;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для работы с учетной записью а также логгинирование
    /// </summary>
    [Route("Api/Account")]
    public class AccountController : BaseApiController
    {
        /// <inheritdoc />
        public AccountController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private AccountManager AccountManager => new AccountManager(ContextWrapper);

        private AccountLoginWorker AccountLoginWorker => new AccountLoginWorker(ContextWrapper);

        #region Методы логинирования

        /// <summary>
        /// Войти по Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/ByEmail")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<LoginResultModel>))]
        public Task<BaseApiResponse<LoginResultModel>> Login([FromForm]LoginModel model)
        {
            return AccountLoginWorker.LoginAsync(model, SignInManager);
        }

        /// <summary>
        /// Войти по номеру телефона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/ByPhone")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<LoginResultModel>))]
        public Task<BaseApiResponse<LoginResultModel>> LoginByPhone([FromForm]LoginByPhoneNumberModel model)
        {
            return AccountLoginWorker.LoginByPhoneNumberAsync(model, SignInManager);
        }

        /// <summary>
        /// Войти под другим пользователем (доступно только исключительным пользователям)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/AsUser")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> LoginAsUser([FromForm]UserIdModel model)
        {
            return AccountLoginWorker.LoginAsUserAsync(model, SignInManager);
        }
        
        #endregion

        #region Методы изменения
        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Change/Password")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> ChangePassword([FromForm]ChangeUserPasswordModel model)
        {
            return AccountManager.ChangePasswordAsync(model, UserManager, SignInManager);
        }
        #endregion

        
        /// <summary>
        /// Проверить изменения пользователя (сравнивает залогиненного с тем же самым только в бд)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost("CheckUserChanges")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<ApplicationUserDto>))]
        public BaseApiResponse<ApplicationUserDto> CheckUserChanges()
        {
            return AccountManager.CheckUserChanges(AuthenticationManager, SignInManager);
        }

        /// <summary>
        /// Разлогиниться в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public BaseApiResponse LogOut()
        {
            return AccountLoginWorker.LogOut(User, AuthenticationManager);
        }
    }
}
